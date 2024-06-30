using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Business.Services.Abstracts;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
    public Task<List<Category>> GetAllParentAsync();
    public Task SendViewBagParentCategories(dynamic ViewBag,int? blockedId=null);
    public Task<bool> CreateAsync(CategoryCreateVm vm, ModelStateDictionary ModelState,dynamic ViewBag,string imagePath);

    public Task<bool> DeleteAsync(int id,string ImagePath);
    public Task<CategoryUpdateVm?> GetUpdatedCategoryAsync(int id,dynamic ViewBag);
    public Task<bool?> UpdateAsync(CategoryUpdateVm vm, ModelStateDictionary ModelState,dynamic ViewBag,string imagePath);
    public Task<Category?> GetByIdAsync(int id);
    public Task<bool> IsExistAsync(Expression<Func<Category,bool>> expression);
  
}
