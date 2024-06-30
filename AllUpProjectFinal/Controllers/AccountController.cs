using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        var result = await _service.LoginAsync(vm,ModelState);

        if(!result)
            return View(vm);

        return RedirectToAction("Index","Home");
    }

    public async Task<IActionResult> Logout()
    {
        var result=await _service.LogoutAsync();

        if (!result)
            return BadRequest();

        return RedirectToAction("Login");
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        var result=await _service.RegisterAsync(vm, ModelState);

        if(!result)
            return View(vm);

        return RedirectToAction("Login");
    }


    public async Task<IActionResult> CreateRoles()
    {
        await _service.CreateRoles();

        return Content("OK");
    }

    public async Task<IActionResult> VerifyEmail(string email,string token)
    {
        var result=await _service.VerifyEmail(email, token);

        if(!result)
            return BadRequest();

        return RedirectToAction("Index","Home");
    }
}

