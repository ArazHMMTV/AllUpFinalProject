using Business.Extensions;
using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Services.Concretes;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _repository;
    private readonly IBlogCategoryService _categoryService;

    public BlogService(IBlogRepository repository, IBlogCategoryService categoryService)
    {
        _repository = repository;
        _categoryService = categoryService;
    }

    public async Task<bool> CreateAsync(BlogCreateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath)
    {
        await SendViewBagElements(ViewBag);

        if (!ModelState.IsValid)
            return false;

        var isExistCategory = await _categoryService.IsExistAsync(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "This category is not found");
            return false;
        }

        if (!vm.Image.CheckImage())
        {
            ModelState.AddModelError("Image", "Please enter valid input");
            return false;
        }


        Blog blog = new()
        {
            Title = vm.Title,
            Author = vm.Author,
            CreatedTime = DateTime.Now,
            Description = vm.Description,
            BlogCategoryId = vm.CategoryId,

        };

        blog.ImagePath = await vm.Image.CreateImageAsync(imagePath);

        await _repository.CreateAsync(blog);
        await _repository.SaveAsync();

        return true;

    }

    public async Task<bool> DeleteAsync(int id, string imagePath)
    {
        var blog = await _repository.GetSingleAsync(x => x.Id == id);

        if (blog is null)
            return false;

        _repository.Delete(blog);
        await _repository.SaveAsync();

        blog.ImagePath.DeleteImage(imagePath);

        return true;

    }

    public async Task<List<Blog>> GetAllAsync()
    {
        var blogs = await _repository.GetAll("BlogCategory").ToListAsync();

        return blogs;
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await _repository.GetSingleAsync(x => x.Id == id,"BlogCategory");
    }

    public async Task<BlogUpdateVm?> GetUpdatedBlogAsync(int id, dynamic ViewBag)
    {
        var blog = await _repository.GetSingleAsync(x => x.Id == id);

        if (blog is null)
            return null;

        BlogUpdateVm vm = new()
        {
            Title = blog.Title,
            ImagePath = blog.ImagePath,
            Description = blog.Description,
            Author = blog.Author,
            CategoryId = blog.BlogCategoryId
        };

        return vm;
    }

    public async Task<bool> IsExistAsync(Expression<Func<Blog, bool>> expression)
    {
        return await _repository.IsExistAsync(expression);
    }

    public async Task SendViewBagElements(dynamic ViewBag)
    {
        ViewBag.Categories = await _categoryService.GetAllAsync();
    }

    public async Task<bool?> UpdateAsync(BlogUpdateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath)
    {
        await SendViewBagElements(ViewBag);

        if (!ModelState.IsValid)
            return false;

        var existedBlog = await _repository.GetSingleAsync(x => x.Id == vm.Id);

        if (existedBlog is null)
            return null;


        var isExistCategory = await _categoryService.IsExistAsync(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "This category is not found");
            return false;
        }


        if (vm.Image is not null && !vm.Image.CheckImage())
        {
            ModelState.AddModelError("Image", "Please enter valid input");
            return false;
        }


        existedBlog.Title = vm.Title;
        existedBlog.Description = vm.Description;
        existedBlog.Author = vm.Author;
        existedBlog.BlogCategoryId = vm.CategoryId;

        if (vm.Image is not null)
        {
            existedBlog.ImagePath.DeleteImage(imagePath);

            existedBlog.ImagePath = await vm.Image.CreateImageAsync(imagePath);
        }


        _repository.Update(existedBlog);
        await _repository.SaveAsync();

        return true;
    }
}
