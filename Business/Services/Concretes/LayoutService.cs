using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;

namespace Business.Services.Concretes;

public class LayoutService : ILayoutService
{
    private readonly ICategoryService _categoryService;
    private readonly ISettingService _settingService;
    private readonly IProductService _productService;
    public LayoutService(ICategoryService categoryService, ISettingService settingService, IProductService productService)
    {
        _categoryService = categoryService;
        _settingService = settingService;
        _productService = productService;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await _categoryService.GetAllParentAsync();

        return categories;
    }

    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        var settings = await _settingService.GetAllSettingsAsync();

        return settings;
    }

    public async Task<List<Product>> SearchAsync(SearchVm vm)
    {
        var products = await _productService.GetAllAsync();

        products = products.Where(x => x.Name.Contains(vm.SearchValue.ToLower())).ToList();

        if(vm.CategoryId is not null)
            products=products.Where(x=>x.CategoryId==vm.CategoryId).ToList();

        return products;
    }
}
