using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _service;
    private readonly IWebHostEnvironment _environment;
    private readonly string _imagePath;


    public CategoryController(ICategoryService service, IWebHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
        _imagePath = Path.Combine(_environment.WebRootPath, "assets", "images");
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _service.GetAllAsync();

        return View(categories);
    }

    public async Task<IActionResult> Create()
    {
        await _service.SendViewBagParentCategories(ViewBag);
        return View();

    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateVm vm)
    {
        var result = await _service.CreateAsync(vm, ModelState, ViewBag, _imagePath);

        if(!result)
            return View(vm);

        return RedirectToAction("Index");
    }
}
