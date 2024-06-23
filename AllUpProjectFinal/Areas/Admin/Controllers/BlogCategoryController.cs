using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogCategoryController : Controller
{
    private readonly IBlogCategoryService _service;

    public BlogCategoryController(IBlogCategoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var categories=await _service.GetAllAsync();

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(BlogCategoryCreateVm vm)
    {
        var result = await _service.CreateAsync(vm, ModelState);

        if(!result)
            return View(vm);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result =await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var result = await _service.GetUpdatedBlogCategoryAsync(id);

        if(result is null)
            return NotFound();

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(BlogCategoryUpdateVm vm)
    {
        var result=await _service.UpdateAsync(vm,ModelState);

        if (result is null)
            return BadRequest();
        else if (result is false)
            return View(vm);

        return RedirectToAction("Index");
    }
}
