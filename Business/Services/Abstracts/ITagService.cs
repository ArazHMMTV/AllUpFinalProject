using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface ITagService
{
    public Task<List<Tag>> GetAllAsync();
    public Task<bool> CreateAsync(TagCreateVm vm,ModelStateDictionary ModelState);

}
