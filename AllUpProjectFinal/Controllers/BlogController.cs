using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IBlogCategoryService _blogCategoryService;
    public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
    {
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
    }

    public async Task<IActionResult> Index()
    {
        BlogVm vm = new()
        {
            BlogCategories=await _blogCategoryService.GetAllAsync(),
            Blogs=await _blogService.GetAllAsync(),
        };

        return View(vm);
    }
}
