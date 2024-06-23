using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Linq.Expressions;

namespace Business.Services.Abstracts;

public interface ITagService
{
    public Task<List<Tag>> GetAllAsync();
    public Task<bool> CreateAsync(TagCreateVm vm, ModelStateDictionary ModelState);

    public Task<bool> DeleteAsync(int id);
    public Task<TagUpdateVm?> GetUpdatedTagAsync(int id);
    public Task<bool?> UpdateAsync(TagUpdateVm vm, ModelStateDictionary ModelState);

    public Task<bool> IsExistAsync(Expression<Func<Tag, bool>> expression);


}
