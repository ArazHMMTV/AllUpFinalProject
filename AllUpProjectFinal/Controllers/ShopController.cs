using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Controllers;

public class ShopController : Controller
{
    private readonly ILayoutService _layoutService;
    private readonly IProductService _productService;

    public ShopController(ILayoutService layoutService, IProductService productService)
    {
        _layoutService = layoutService;
        _productService = productService;
    }

    public async Task<IActionResult> Index(int? categoryId=null)
    {
        var products=await _productService.GetAllAsync(categoryId);
        return View(products);
    }

    public async Task<IActionResult> Search(SearchVm vm)
    {
        if (!ModelState.IsValid)
            return Redirect(vm.ReturnUrl);

        var result = await _layoutService.SearchAsync(vm);

        return View("Index", result);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var result=await _productService.GetByIdAsync(id);

        if(result is null)
            return NotFound();

        return View(result);
    }
}
