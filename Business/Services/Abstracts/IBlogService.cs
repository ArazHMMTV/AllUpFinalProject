using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Business.Services.Abstracts;

public interface IBlogService
{
    public Task<List<Blog>> GetAllAsync();
    public Task<bool> CreateAsync(BlogCreateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath);

    public Task<bool> DeleteAsync(int id, string imagePath);
    public Task<BlogUpdateVm?> GetUpdatedBlogAsync(int id, dynamic ViewBag);
    public Task<bool?> UpdateAsync(BlogUpdateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath);
    public Task<Blog?> GetByIdAsync(int id);
    public Task<bool> IsExistAsync(Expression<Func<Blog, bool>> expression);

    public Task SendViewBagElements(dynamic ViewBag);
}
