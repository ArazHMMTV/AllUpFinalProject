using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Business.Services.Abstracts;

public interface IBlogCategoryService
{
    public Task<List<BlogCategory>> GetAllAsync();
    public Task<bool> CreateAsync(BlogCategoryCreateVm vm, ModelStateDictionary ModelState);

    public Task<bool> DeleteAsync(int id);
    public Task<BlogCategoryUpdateVm?> GetUpdatedBlogCategoryAsync(int id);
    public Task<bool?> UpdateAsync(BlogCategoryUpdateVm vm, ModelStateDictionary ModelState);
    public Task<BlogCategory?> GetByIdAsync(int id);
    public Task<bool> IsExistAsync(Expression<Func<BlogCategory, bool>> expression);
}
