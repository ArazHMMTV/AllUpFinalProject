using Business.Extensions;
using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;

namespace Business.Services.Concretes;
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;
    private readonly IBrandService _brandService;
    public ProductService(IProductRepository repository, ICategoryService categoryService, ITagService tagService, IBrandService brandService)
    {
        _repository = repository;
        _categoryService = categoryService;
        _tagService = tagService;
        _brandService = brandService;
    }

    public async Task<bool> CreateAsync(ProductCreateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath)
    {
        await SendViewBagElements(ViewBag);

        if (!ModelState.IsValid)
            return false;

        #region Product Validations


        var isExistCategory = await _categoryService.IsExistAsync(x => x.Id == vm.CategoryId && x.Children.Count == 0);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Category is not found");
            return false;
        }

        var isExistBrand = await _brandService.IsExistAsync(x => x.Id == vm.BrandId);

        if (!isExistBrand)
        {
            ModelState.AddModelError("BrandId", "Brand is not found");
            return false;
        }

        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = await _tagService.IsExistAsync(x => x.Id == tagId);

            if (!isExistTag)
            {
                ModelState.AddModelError("TagIds", "Tag is not found");
                return false;
            }

        }



        if (!vm.MainImage.CheckImage())
        {
            ModelState.AddModelError("MainImage", "Please enter valid input");
            return false;
        }

        if (!vm.HoverImage.CheckImage())
        {
            ModelState.AddModelError("HoverImage", "Please enter valid input");
            return false;
        }

        foreach (var image in vm.Images)
        {
            if (!image.CheckImage())
            {
                ModelState.AddModelError("Images", "Please enter valid input");
                return false;
            }
        }



        #endregion


        Product product = new()
        {
            Name = vm.Name,
            LongDescription = vm.LongDescription,
            BrandId = vm.BrandId,
            CategoryId = vm.CategoryId,
            Discount = vm.Discount,
            Price = vm.Price,
            ProductCode = vm.ProductCode,
            Quantity = vm.Quantity,
            Rating = vm.Rating,
            ShortDescription = vm.ShortDescription,
        };

        #region Create Images


        product.ProductImages = new List<ProductImage>();


        string mainImagePath = await vm.MainImage.CreateImageAsync(imagePath);
        product.ProductImages.Add(new() { IsMain = true, Path = mainImagePath, Product = product });


        string hoverImagePath = await vm.HoverImage.CreateImageAsync(imagePath);
        product.ProductImages.Add(new() { IsHover = true, Path = hoverImagePath, Product = product });

        foreach (var item in vm.Images)
        {
            string imgPath = await item.CreateImageAsync(imagePath);
            product.ProductImages.Add(new() { Path = imgPath, Product = product });
        }

        #endregion


        #region Create ProductTags

        product.ProductTags = new List<ProductTag>();


        foreach (var tagId in vm.TagIds)
        {
            product.ProductTags.Add(new()
            {
                TagId = tagId,
                Product = product
            });
        }

        #endregion

        await _repository.CreateAsync(product);
        await _repository.SaveAsync();


        return true;
    }

    public async Task<bool> DeleteAsync(int id, string ImagePath)
    {
        var product = await _repository.GetSingleAsync(x => x.Id == id, "ProductImages");

        if (product is null)
            return false;

        foreach (var image in product.ProductImages)
            image.Path.DeleteImage(ImagePath);

        _repository.Delete(product);
        await _repository.SaveAsync();

        return true;

    }

    public async Task<List<Product>> GetAllAsync(int? categoryId = null)
    {
        var products = await _repository.GetAll("ProductImages", "Category").ToListAsync();

        if (categoryId is not null)
            products = products.Where(x => x.CategoryId == categoryId || x.Category.ParentId == categoryId).ToList();

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _getProductById(id);
    }

    public async Task<ProductUpdateVm?> GetUpdatedProductAsync(int id, dynamic ViewBag)
    {
        var product = await _getProductById(id);

        if (product is null)
            return null;

        ProductUpdateVm vm = new()
        {
            Id = id,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            Discount = product.Discount,
            LongDescription = product.LongDescription,
            Name = product.Name,
            Price = product.Price,
            ProductCode = product.ProductCode,
            Quantity = product.Quantity,
            Rating = product.Rating,
            ShortDescription = product.ShortDescription,
            TagIds = product.ProductTags.Select(x => x.TagId).ToList(),
            ImagePaths = product.ProductImages.Where(x => !x.IsMain && !x.IsHover).Select(x => x.Path).ToList(),
            ImageIds = product.ProductImages.Where(x => !x.IsMain && !x.IsHover).Select(x => x.Id).ToList(),
            HoverImagePath = product.ProductImages.FirstOrDefault(x => x.IsHover)?.Path ?? "null",
            MainImagePath = product.ProductImages.FirstOrDefault(x => x.IsMain)?.Path ?? "null",
        };

        await SendViewBagElements(ViewBag);

        return vm;
    }


    public async Task SendViewBagElements(dynamic ViewBag)
    {
        var categories = (await _categoryService.GetAllAsync()).Where(x => x.Children?.Count == 0).ToList();
        var brands = await _brandService.GetAllAsync();
        var tags = await _tagService.GetAllAsync();

        ViewBag.Categories = categories;
        ViewBag.Brands = brands;
        ViewBag.Tags = tags;
    }

    public async Task<bool?> UpdateAsync(ProductUpdateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath)
    {
        await SendViewBagElements(ViewBag);

        if (!ModelState.IsValid)
            return false;

        var existProduct = await _getProductById(vm.Id);

        if (existProduct is null)
            return null;



        #region Product Validations


        var isExistCategory = await _categoryService.IsExistAsync(x => x.Id == vm.CategoryId && x.Children.Count == 0);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Category is not found");
            return false;
        }

        var isExistBrand = await _brandService.IsExistAsync(x => x.Id == vm.BrandId);

        if (!isExistBrand)
        {
            ModelState.AddModelError("BrandId", "Brand is not found");
            return false;
        }

        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = await _tagService.IsExistAsync(x => x.Id == tagId);

            if (!isExistTag)
            {
                ModelState.AddModelError("TagIds", "Tag is not found");
                return false;
            }

        }



        if (vm.MainImage is not null && !vm.MainImage.CheckImage())
        {
            ModelState.AddModelError("MainImage", "Please enter valid input");
            return false;
        }

        if (vm.HoverImage is not null && !vm.HoverImage.CheckImage())
        {
            ModelState.AddModelError("HoverImage", "Please enter valid input");
            return false;
        }

        foreach (var image in vm.Images)
        {
            if (!image.CheckImage())
            {
                ModelState.AddModelError("Images", "Please enter valid input");
                return false;
            }
        }



        #endregion



        existProduct.Name = vm.Name;
        existProduct.Price = vm.Price;
        existProduct.CategoryId = vm.CategoryId;
        existProduct.BrandId = vm.BrandId;
        existProduct.Discount = vm.Discount;
        existProduct.Quantity = vm.Quantity;
        existProduct.Price = vm.Price;
        existProduct.Rating = vm.Rating;
        existProduct.LongDescription = vm.LongDescription;
        existProduct.ShortDescription = vm.ShortDescription;
        existProduct.ProductCode = vm.ProductCode;

        existProduct.ProductTags = new List<ProductTag>();

        foreach (var tagId in vm.TagIds)
        {
            existProduct.ProductTags.Add(new()
            {
                TagId = tagId,
                Product = existProduct
            });
        }


        #region CreateNewImages



        if (vm.MainImage is not null)
        {
            existProduct.ProductImages.FirstOrDefault(x => x.IsMain)?.Path.DeleteImage(imagePath);
            existProduct.ProductImages.FirstOrDefault(x => x.IsMain).Path = await vm.MainImage.CreateImageAsync(imagePath);
        }


        if (vm.HoverImage is not null)
        {
            existProduct.ProductImages.FirstOrDefault(x => x.IsHover)?.Path.DeleteImage(imagePath);
            existProduct.ProductImages.FirstOrDefault(x => x.IsHover).Path = await vm.HoverImage.CreateImageAsync(imagePath);
        }




        var ExistedImages = existProduct.ProductImages.Where(x => !x.IsMain && !x.IsHover).Select(x => x.Id).ToList();
        if (vm.ImageIds is not null)
        {
            ExistedImages = ExistedImages.Except(vm.ImageIds).ToList();

        }
        if (ExistedImages.Count > 0)
        {
            foreach (var imageId in ExistedImages)
            {
                var deletedImage = existProduct.ProductImages.FirstOrDefault(x => x.Id == imageId);
                if (deletedImage is not null)
                {

                    existProduct.ProductImages.Remove(deletedImage);
                    deletedImage.Path.DeleteImage(imagePath);

                }

            }
        }


        foreach (var image in vm.Images)
        {
            existProduct.ProductImages.Add(new() { Path = await image.CreateImageAsync(imagePath), Product = existProduct });
        }

        #endregion

        _repository.Update(existProduct);
        await _repository.SaveAsync();

        return true;
    }

    private async Task<Product?> _getProductById(int id)
    {
        return await _repository.GetSingleAsync(x => x.Id == id, "ProductImages", "Category", "Brand", "ProductTags.Tag");
    }

}
