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

  
    public IActionResult Index()
    {
        return View();
    }
}
