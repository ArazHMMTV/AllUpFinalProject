using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductService _service;
    private readonly IWebHostEnvironment _environment;
    private readonly string _imagePath;
    public ProductController(IProductService service, IWebHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
        _imagePath = Path.Combine(_environment.WebRootPath, "assets", "images");
    }

    public async Task<IActionResult> Index()
    {
        var products=await _service.GetAllAsync();

        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        await _service.SendViewBagElements(ViewBag);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVm vm)
    {
        var result = await _service.CreateAsync(vm, ModelState, ViewBag, _imagePath);

        if(!result)
            return View(vm);

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {
        var result = await _service.GetUpdatedProductAsync(id, ViewBag);

        if (result is null)
            return NotFound();

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateVm vm)
    {
        var result=await _service.UpdateAsync(vm,ModelState, ViewBag, _imagePath);

        if (result is null)
            return BadRequest();
        else if(result is false)
            return View(vm);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result=await _service.DeleteAsync(id,_imagePath);

        if(result is false)
            return NotFound();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Detail(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result is null)
            return NotFound();


        return View(result);

    }
}
