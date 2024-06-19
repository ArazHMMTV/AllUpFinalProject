using AspNetCore;
using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Areas.Admin.Controllers;
[Area("Admin")]
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
}
