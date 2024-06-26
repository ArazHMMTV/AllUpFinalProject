﻿using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface IProductService
{
    public Task<List<Product>> GetAllAsync();
    public Task SendViewBagElements(dynamic ViewBag);
    public Task<bool> CreateAsync(ProductCreateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath);

    public Task<bool> DeleteAsync(int id, string ImagePath);
    public Task<ProductUpdateVm?> GetUpdatedProductAsync(int id, dynamic ViewBag);
    public Task<bool?> UpdateAsync(ProductUpdateVm vm, ModelStateDictionary ModelState, dynamic ViewBag, string imagePath);
    public Task<Product?> GetByIdAsync(int id);



}
