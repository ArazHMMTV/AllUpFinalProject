using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class TagController : Controller
{
    private readonly ITagService _service;

    public TagController(ITagService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var tags = await _service.GetAllAsync();

        return View(tags);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(TagCreateVm vm)
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
        var result = await _service.GetUpdatedTagAsync(id);

        if (result is null)
            return NotFound();


        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TagUpdateVm vm)
    {
        var result = await _service.UpdateAsync(vm, ModelState);

        if (result is null)
            return NotFound();

        if (result is false)
            return View(vm);

        return RedirectToAction("Index");
    }

}
