using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface IBrandService
{
    public Task<List<Brand>> GetAllAsync();
    public Task<bool> CreateAsync(BrandCreateVm vm, ModelStateDictionary ModelState);

    public Task<bool> DeleteAsync(int id);
    public Task<BrandUpdateVm?> GetUpdatedBrandAsync(int id);
    public Task<bool?> UpdateAsync(BrandUpdateVm vm, ModelStateDictionary ModelState);

}