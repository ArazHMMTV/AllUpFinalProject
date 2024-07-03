using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SettingController : Controller
{
    private readonly ISettingService _service;

    public SettingController(ISettingService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {

        var settings = await _service.GetAllAsync();

        return View(settings);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Setting setting)
    {
        var result = await _service.CreateAsync(setting, ModelState);

        if (!result)
            return View(setting);


        return RedirectToAction("Index");

    }


    public async Task<IActionResult> Delete(int id)
    {
        var result=await _service.DeleteAsync(id);

        if(!result) 
            return NotFound();  

        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Update(int id)
    {
        var result=await _service.GetUpdatedSettingAsync(id);

        if(result is null)
            return NotFound();

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Setting setting)
    {
        var result=await _service.UpdateAsync(setting,ModelState);

        if (result is null)
            return BadRequest();
        else if (result is false)
            return View(setting);

        return RedirectToAction("Index");
    }
}
