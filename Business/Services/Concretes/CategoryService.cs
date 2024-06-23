using Business.Extensions;
using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
   
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(CategoryCreateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath)
    {
        await SendViewBagParentCategories(ViewBag);

        if (!ModelState.IsValid)
            return false;

        if (vm.ParentId is not null)
        {
            var parentCategory = await _repository.GetSingleAsync(x => x.Id == vm.ParentId && x.Parent == null, "Parent");

            if (parentCategory is null)
            {
                ModelState.AddModelError("ParentId", "This category is not found");
                return false;
            }

        }

        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Name", "This category is already exist");
            return false;
        }


        if (!vm.Image.CheckImage())
        {
            ModelState.AddModelError("Image", "Please enter valid format");
            return false;
        }

        string filename=await vm.Image.CreateImage(imagePath);

        Category category = new()
        {
            Name = vm.Name,
            ParentId = vm.ParentId,
            ImageUrl = filename

        };

        await _repository.CreateAsync(category);
        await _repository.SaveAsync();

        return true;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllAsync()
    {
        var categories = await _repository.GetAll().ToListAsync();

        return categories;
    }

    public Task<CategoryUpdateVm?> GetUpdatedCategoryAsync(int id, dynamic ViewBag)
    {
        throw new NotImplementedException();
    }

    public async Task SendViewBagParentCategories(dynamic ViewBag)
    {
        var parentCategories = await _repository.GetFiltered(x => x.ParentId == null).ToListAsync();
        
        ViewBag.Categories = parentCategories;    
    }

    public Task<bool?> UpdateAsync(CategoryUpdateVm vm, ModelStateDictionary ModelState)
    {
        throw new NotImplementedException();
    }
}
