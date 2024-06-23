using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly IBlogService _service;
    private readonly IWebHostEnvironment _environment;
    private readonly string _imagePath;

    public BlogController(IBlogService service, IWebHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
        _imagePath = Path.Combine(_environment.WebRootPath, "assets", "images");
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _service.GetAllAsync();

        return View(blogs);
    }

    public async Task<IActionResult> Create()
    {
        await _service.SendViewBagElements(ViewBag);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateVm vm)
    {
        var result = await _service.CreateAsync(vm, ModelState, ViewBag, _imagePath);

        if (!result)
            return View(vm);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id, _imagePath);

        if (!result)
            return NotFound();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {

        var result=await _service.GetUpdatedBlogAsync(id,ViewBag);

        if(result is null)
            return NotFound();


        await _service.SendViewBagElements(ViewBag);

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(BlogUpdateVm vm)
    {
        var result = await _service.UpdateAsync(vm, ModelState, ViewBag, _imagePath);

        if (result is null)
            return BadRequest();
        else if (!result)
            return View(vm);

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
