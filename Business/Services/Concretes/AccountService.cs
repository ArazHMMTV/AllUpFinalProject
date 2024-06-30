using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Enums;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concretes;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUrlHelper _urlHelper;
    private readonly IEmailService _emailService;
    public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        _emailService = emailService;
    }

    public async Task<bool> LoginAsync(LoginVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var user = await _userManager.FindByEmailAsync(vm.Email);

        if (user is null)
        {
            ModelState.AddModelError("", "Wrong email or password");
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

        if (!result.Succeeded)
        {
            if (!user.EmailConfirmed)
                ModelState.AddModelError("", "Please confirm email");
            else if (result.IsLockedOut)
                ModelState.AddModelError("", "User is blocked,please try again 3 minute later");
            else
                ModelState.AddModelError("", "Wrong email or password");
            return false;
        }


        return true;
    }

    public async Task<bool> LogoutAsync()
    {
        if (!_contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
            return false;

        await _signInManager.SignOutAsync();

        return true;

    }

    public async Task<bool> RegisterAsync(RegisterVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;


        AppUser user = new()
        {
            LastName = vm.LastName,
            FirstName = vm.FirstName,
            Email = vm.Email,
            UserName = vm.Username
        };

        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", string.Join(',', result.Errors.Select(x => x.Description)));
            return false;
        }


        await _userManager.AddToRoleAsync(user, IdentityRoles.Member.ToString());

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = _urlHelper.Action("VerifyEmail", "Account", new { email = user.Email, token = token }, _contextAccessor.HttpContext.Request.Scheme);


        string emailBody = @$"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Confirm Your Email Address</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #007bff; /* Blue base color */
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 50px auto;
            background-color: #ffffff; /* White base color */
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .message {{
            color: #333;
            margin-bottom: 20px;
        }}
        .confirmation-link {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #0056b3; /* Darker blue for link */
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
        }}
        .confirmation-link:hover {{
            background-color: #003d7a; /* Even darker blue on hover */
        }}

        a{{
            font-style: none;

        }}
    </style>
</head>
<body>
    <div class=""container"">
        <p class=""message"">Dear {user.UserName},</p>
        <p class=""message"">Please click the following link to confirm your email address and complete your registration:</p>
        <a href=""{link}"" class=""confirmation-link"">Confirm Email</a>
        <p class=""message"">If you did not request this, please ignore this email.</p>
        <p class=""message"">Regards,<br>All Up Team</p>
    </div>
</body>
</html>";


        _emailService.SendEmail(new(body: emailBody, subject: "Email Verification", to: vm.Email));

        return true;
    }




    public async Task<bool> VerifyEmail(string? email, string? token)
    {
        if (token == null || email == null)
            return false;

        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == email);

        if (user is null)
            return false;

        var verify = await _userManager.ConfirmEmailAsync(user, token);
        if (verify.Succeeded)
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignInAsync(user, false);
            return true;
        }
        return false;
    }

    public async Task CreateRoles()
    {

        foreach (var role in Enum.GetNames(typeof(IdentityRoles)))
        {
            await _roleManager.CreateAsync(new() { Name = role });
        }
    }
}
