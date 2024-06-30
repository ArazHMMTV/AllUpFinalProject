using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AllUpProjectFinal.Controllers;

public class HomeController : Controller
{
    private readonly ILayoutService _layoutService;

    public HomeController(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    public async Task<IActionResult> Search(SearchVm vm)
    {
        if(!ModelState.IsValid)
            return RedirectToAction(vm.ReturnUrl);

        var result=await _layoutService.SearchAsync(vm);

        return View("Index","Shop");
    }
    public IActionResult Index()
    {
        return View();
    }
}
