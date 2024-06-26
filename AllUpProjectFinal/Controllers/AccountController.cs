using Business.Services.Abstracts;
using Business.ViewModels;
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
}

