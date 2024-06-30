using Business.Extensions;
using Business.Services.Abstracts;
using Business.ViewModels;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Services.Concretes;

public class SliderService : ISliderService
{
    private readonly ISliderRepository _repository;

    public SliderService(ISliderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(SliderCreateVm vm, ModelStateDictionary ModelState, string imagePath)
    {
        if (!ModelState.IsValid)
            return false;

        if (!vm.Image.CheckImage())
        {
            ModelState.AddModelError("Image", "Please reenter valid format");
            return false;
        }
        string filename = await vm.Image.CreateImageAsync(imagePath);

        Slider slider = new()
        {
            Title = vm.Title,
            Description = vm.Description,
            Subtitle = vm.Subtitle,
            ImagePath = filename
        };

        await _repository.CreateAsync(slider);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id, string imagePath)
    {
        var slider = await _repository.GetSingleAsync(x => x.Id == id);

        if (slider is null)
            return false;

        _repository.Delete(slider);
        await _repository.SaveAsync();

        slider.ImagePath.DeleteImage(imagePath);

        return true;
    }

    public async Task<List<Slider>> GetAllAsync()
    {
        var sliders = await _repository.GetAll().ToListAsync();

        return sliders;
    }

    public async Task<Slider?> GetByIdAsync(int id)
    {
        var slider = await _repository.GetSingleAsync(x => x.Id == id);

        return slider;
    }

    public async Task<SliderUpdateVm?> GetUpdatedSliderAsync(int id)
    {
        var slider = await _repository.GetSingleAsync(x => x.Id == id);

        if (slider is null)
            return null;

        SliderUpdateVm sliderUpdateVm = new()
        {
            ImagePath = slider.ImagePath,
            Description = slider.Description,
            Title = slider.Title,
            Subtitle = slider.Subtitle,
        };

        return sliderUpdateVm;
    }

    public async Task<bool> IsExistAsync(Expression<Func<Slider, bool>> expression)
    {
        return await _repository.IsExistAsync(expression);
    }

    public async Task<bool?> UpdateAsync(SliderUpdateVm vm, ModelStateDictionary ModelState, string imagePath)
    {
        if (!ModelState.IsValid)
            return false;

        var existSlider = await _repository.GetSingleAsync(x => x.Id == vm.Id);

        if (existSlider is null)
            return null;

        if (vm.Image is not null && !vm.Image.CheckImage())
        {
            ModelState.AddModelError("Image", "Please reenter valid input");
            return false;
        }

        existSlider.Subtitle = vm.Subtitle;
        existSlider.Title = vm.Title;
        existSlider.Description = vm.Description;
        if (vm.Image is not null)
            existSlider.ImagePath = await vm.Image.CreateImageAsync(imagePath);

        _repository.Update(existSlider);
        await _repository.SaveAsync();

        return true;

    }
}
