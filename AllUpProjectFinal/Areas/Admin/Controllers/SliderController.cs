﻿using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SliderController : Controller
{
    private readonly ISliderService _service;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _imagePath;
    public SliderController(ISliderService service, IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _webHostEnvironment = webHostEnvironment;
        _imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images");
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _service.GetAllAsync();

        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(SliderCreateVm vm)
    {
        var result = await _service.CreateAsync(vm, ModelState, _imagePath);

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
        var result = await _service.GetUpdatedSliderAsync(id);

        if(result is null)
            return NotFound();

        return View(result);

    }


    [HttpPost]
    public async Task<IActionResult> Update(SliderUpdateVm vm)
    {
        var result=await _service.UpdateAsync(vm,ModelState, _imagePath);

        if(result is null)
            return NotFound();
        else if(result is false)
            return View(vm);

        return RedirectToAction("Index");
    }
}
