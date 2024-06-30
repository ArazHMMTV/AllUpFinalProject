using Business.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Business.Services.Abstracts;

public interface ISliderService
{
    public Task<List<Slider>> GetAllAsync();
    public Task<bool> CreateAsync(SliderCreateVm vm, ModelStateDictionary ModelState,  string imagePath);

    public Task<bool> DeleteAsync(int id, string imagePath);
    public Task<SliderUpdateVm?> GetUpdatedSliderAsync(int id);
    public Task<bool?> UpdateAsync(SliderUpdateVm vm, ModelStateDictionary ModelState,  string imagePath);
    public Task<Slider?> GetByIdAsync(int id);
    public Task<bool> IsExistAsync(Expression<Func<Slider, bool>> expression);

}
