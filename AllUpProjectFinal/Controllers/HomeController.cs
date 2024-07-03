using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AllUpProjectFinal.Controllers;

public class HomeController : Controller
{
    private readonly ILayoutService _layoutService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IBlogService _blogService;
    private readonly ISliderService _sliderService;
    public HomeController(ILayoutService layoutService, ICategoryService categoryService, IProductService productService, IBlogService blogService, ISliderService sliderService)
    {
        _layoutService = layoutService;
        _categoryService = categoryService;
        _productService = productService;
        _blogService = blogService;
        _sliderService = sliderService;
    }


    public async Task<IActionResult> Index()
    {
        HomeVm vm = new()
        {
            Sliders = await _sliderService.GetAllAsync(),
            BestProducts = await _productService.GetBestProducts(),
            BestsellerProducts = await _productService.GetBestSellerProducts(),
            Categories = (await _categoryService.GetAllAsync()).Take(12).ToList(),
            Blogs = (await _blogService.GetAllAsync()).Take(3).ToList(),
            NewArrival = await _productService.GetNewProducts(),

        };

        return View(vm);
    }
}
