using Business.Extensions;
using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        string filename = await vm.Image.CreateImageAsync(imagePath);

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

    public async Task<bool> DeleteAsync(int id, string imagePath)
    {
        var category = await _repository.GetSingleAsync(x => x.Id == id, "Children", "Products");

        if (category is null)
            return false;

        if (category.Children?.Count > 0 || category.Products?.Count > 0)
            return false;

        category.ImageUrl.DeleteImage(imagePath);

        _repository.Delete(category);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        var categories = await _repository.GetAll("Children", "Products").ToListAsync();

        return categories;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        var category = await _repository.GetSingleAsync(x => x.Id == id, "Parent", "Children", "Products.ProductImages");

        return category;
    }

    public async Task<CategoryUpdateVm?> GetUpdatedCategoryAsync(int id, dynamic ViewBag)
    {

        var category = await _repository.GetSingleAsync(x => x.Id == id, "Children", "Products");

        if (category is null)
            return null;


        if (category.Children?.Count == 0 && category.Products?.Count == 0)
            await SendViewBagParentCategories(ViewBag, id);
        else
            ViewBag.Categories = new List<Category>();

        CategoryUpdateVm vm = new()
        {
            Id = id,
            Name = category.Name,
            ImageUrl = category.ImageUrl,
            ParentId = category.ParentId
        };

        return vm;
    }

    public async Task<bool> IsExistAsync(Expression<Func<Category, bool>> expression)
    {
        return await _repository.IsExistAsync(expression,"Children","Products");
    }

    public async Task SendViewBagParentCategories(dynamic ViewBag, int? blockedId = null)
    {
        var query = _repository.GetFiltered(x => x.ParentId == null);

        if (blockedId is not null)
            query = query.Where(x => x.Id != blockedId);

        var parentCategories = await query.ToListAsync();

        ViewBag.Categories = parentCategories;
    }

    public async Task<bool?> UpdateAsync(CategoryUpdateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath)
    {
        var existCategory = await _repository.GetSingleAsync(x => x.Id == vm.Id, "Children", "Parent", "Products");

        if (existCategory is null)
            return null;


        if (existCategory.Children?.Count == 0 && existCategory.Products?.Count == 0)
            await SendViewBagParentCategories(ViewBag, vm.Id);
        else
            ViewBag.Categories = new List<Category>();

        vm.ImageUrl = existCategory.ImageUrl;

        if (!ModelState.IsValid)
            return false;

        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower() && x.Id != vm.Id);

        if (isExist)
        {
            ModelState.AddModelError("Name", "This category is already exist");
            return false;
        }

        if (vm.ParentId is not null)
        {
            if (existCategory.Children?.Count != 0)
            {
                ModelState.AddModelError("", "This is parent category!");
                return false;
            }
            if (existCategory.Products?.Count != 0)
            {
                ModelState.AddModelError("", "This category have products");
                return false;
            }
            var parentCategory = await _repository.GetSingleAsync(x => x.Id == vm.ParentId && x.Parent == null && x.Id != vm.Id && x.Products.Count==0 ,"Parent","Products");

            if (parentCategory is null)
            {
                ModelState.AddModelError("ParentId", "This category is not found");
                return false;
            }

        }

        if (vm.Image is not null)
        {
            if (!vm.Image.CheckImage())
            {
                ModelState.AddModelError("Image", "Please enter the valid input");
                return false;
            }

            string filename = await vm.Image.CreateImageAsync(imagePath);
            existCategory.ImageUrl.DeleteImage(imagePath);
            existCategory.ImageUrl = filename;
        }

        existCategory.Name = vm.Name;
        existCategory.ParentId = vm.ParentId;

        _repository.Update(existCategory);
        await _repository.SaveAsync();

        return true;

    }
}
