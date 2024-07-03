using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface IProductService
{
    public Task<List<Product>> GetAllAsync(int? categoryId=null);
    public Task SendViewBagElements(dynamic ViewBag);
    public Task<bool> CreateAsync(ProductCreateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath);

    public Task<bool> DeleteAsync(int id, string ImagePath);
    public Task<ProductUpdateVm?> GetUpdatedProductAsync(int id, dynamic ViewBag);
    public Task<bool?> UpdateAsync(ProductUpdateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath);
    public Task<Product?> GetByIdAsync(int id);

    public Task<List<Product>> GetBestProducts();
    public Task<List<Product>> GetProductsByCategoryId(int categoryId);
    public Task<List<Product>> GetNewProducts();
    public Task<List<Product>> GetBestSellerProducts();

}
