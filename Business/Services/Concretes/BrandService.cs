using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concretes;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;

    public BrandService(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(BrandCreateVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Name", "This Brand is already exist");
            return false;
        }

        Brand brand = new()
        {
            Name = vm.Name,
        };


        await _repository.CreateAsync(brand);
        await _repository.SaveAsync();


        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var brand = await _repository.GetSingleAsync(x => x.Id == id);

        if (brand is null)
            return false;

        _repository.Delete(brand);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        var brands = await _repository.GetAll().ToListAsync();


        return brands;
    }

    public async Task<BrandUpdateVm?> GetUpdatedBrandAsync(int id)
    {
        var brand = await _repository.GetSingleAsync(x => x.Id == id);

        if (brand is null)
            return null;

        BrandUpdateVm vm = new()
        {
            Id = id,
            Name = brand.Name,
        };

        return vm;
    }


    public async Task<bool?> UpdateAsync(BrandUpdateVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;


        var existBrand = await _repository.GetSingleAsync(x => x.Id == vm.Id);

        if (existBrand is null)
            return null;

        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower() && x.Id != vm.Id);

        if (isExist)
        {
            ModelState.AddModelError("Name", "This brand is already exist");
            return false;
        }

        existBrand.Name = vm.Name;

        _repository.Update(existBrand); 
        await _repository.SaveAsync();

        return true;
    }
}