using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concretes;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(TagCreateVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;
        
        var isExist=await _repository.IsExistAsync(x=>x.Name.ToLower()==vm.Name.ToLower());

        if (isExist) {

            ModelState.AddModelError("Name", "This tag is already exist");
            return false;
        }

        Tag tag = new()
        {
            Name = vm.Name,
        };

        await _repository.CreateAsync(tag);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        var tags = await _repository.GetAll().ToListAsync();
        return tags;
    }
}
