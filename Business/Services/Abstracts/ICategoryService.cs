using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
    public Task SendViewBagParentCategories(dynamic ViewBag);
    public Task<bool> CreateAsync(CategoryCreateVm vm, ModelStateDictionary ModelState,dynamic ViewBag,string imagePath);

    public Task<bool> DeleteAsync(int id);
    public Task<CategoryUpdateVm?> GetUpdatedCategoryAsync(int id,dynamic ViewBag);
    public Task<bool?> UpdateAsync(CategoryUpdateVm vm, ModelStateDictionary ModelState);


}
