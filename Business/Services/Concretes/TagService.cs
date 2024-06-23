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

        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower());

        if (isExist)
        {

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

    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await _repository.GetSingleAsync(x => x.Id == id);

        if (tag is null)
            return false;

        _repository.Delete(tag);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        var tags = await _repository.GetAll().ToListAsync();
        return tags;
    }

    public async Task<TagUpdateVm?> GetUpdatedTagAsync(int id)
    {
        var tag = await _repository.GetSingleAsync(x => x.Id == id);

        if (tag is null)
            return null;

        TagUpdateVm vm = new()
        {
            Id = id,
            Name = tag.Name,
        };
        return vm;
    }

    public async Task<bool?> UpdateAsync(TagUpdateVm vm, ModelStateDictionary ModelState)
    {
        var existedTag = await _repository.GetSingleAsync(x => x.Id == vm.Id);

        if (existedTag is null)
            return null;

        if (!ModelState.IsValid)
            return false;

        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == vm.Name.ToLower() && x.Id != vm.Id);


        if (isExist)
        {
            ModelState.AddModelError("Name", "This name is already exist");
            return false;
        }

        existedTag.Name=vm.Name;
         
        _repository.Update(existedTag);
        await _repository.SaveAsync();  


        return true;


    }
}
