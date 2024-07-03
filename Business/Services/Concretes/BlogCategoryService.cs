using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Services.Concretes;

public class BlogCategoryService : IBlogCategoryService
{
    private readonly IBlogCategoryRepository _repository;

    public BlogCategoryService(IBlogCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(BlogCategoryCreateVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;


        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Name", "This category is already exist");
            return false;

        }

        BlogCategory blogCategory = new() { Name = vm.Name };

        await _repository.CreateAsync(blogCategory);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var blogCategory = await _repository.GetSingleAsync(x => x.Id == id);

        if (blogCategory is null)
            return false;

        _repository.Delete(blogCategory);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<List<BlogCategory>> GetAllAsync()
    {
        var categories = await _repository.GetAll("Blogs").ToListAsync();

        return categories;
    }

    public async Task<BlogCategory?> GetByIdAsync(int id)
    {
        return await _repository.GetSingleAsync(x => x.Id == id);
    }

    public async Task<BlogCategoryUpdateVm?> GetUpdatedBlogCategoryAsync(int id)
    {
        var category = await _repository.GetSingleAsync(x => x.Id == id);

        if(category is null)
            return null;

        BlogCategoryUpdateVm vm = new()
        {
            Id = id,
            Name = category.Name,
        };

        return vm;
    }

    public async Task<bool> IsExistAsync(Expression<Func<BlogCategory, bool>> expression)
    {
        return await _repository.IsExistAsync(expression);
    }

    public async Task<bool?> UpdateAsync(BlogCategoryUpdateVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;
        
        var existedCategory=await _repository.GetSingleAsync(x=>x.Id== vm.Id);
        
        if (existedCategory is null)
            return null;

        var isExist=await _repository.IsExistAsync(x=>x.Name.ToLower()==vm.Name.ToLower() && x.Id!=vm.Id);

        if (isExist)
        {
            ModelState.AddModelError("Name", "This category is already exist");
            return false;
        }

        existedCategory.Name=vm.Name;   

        _repository.Update(existedCategory);
        await _repository.SaveAsync();

        return true;
    }
}
