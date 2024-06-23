using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
public class BrandController : Controller
{
    private readonly IBrandService _service;
    public BrandController(IBrandService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var Brands = await _service.GetAllAsync();
        
        return View(Brands);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BrandCreateVm vm)
    {
        var result = await _service.CreateAsync(vm, ModelState);

        if (!result)
            return View(vm);

        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction("Index");

    }


    public async Task<IActionResult> Update(int id)
    {
        var result = await _service.GetUpdatedBrandAsync(id);

        if (result is null)
            return NotFound();


        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(BrandUpdateVm vm)
    {
        var result = await _service.UpdateAsync(vm, ModelState);

        if (result is null)
            return NotFound();

        if (result is false)
            return View(vm);

        return RedirectToAction("Index");
    }

}
