using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concretes;

public class SettingService : ISettingService
{
    private readonly ISettingRepository _repository;

    public SettingService(ISettingRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(Setting setting, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var isExist = await _repository.IsExistAsync(x => x.Key.ToLower() == setting.Key.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Key", "This key is already exist");
            return false;
        }

        await _repository.CreateAsync(setting);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var setting = await _repository.GetSingleAsync(x => x.Id == id);

        if (setting is null)
            return false;

        _repository.Delete(setting);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<List<Setting>> GetAllAsync()
    {
        var settings = await _repository.GetAll().ToListAsync();

        return settings;
    }

    public async Task<Dictionary<string, string>> GetAllSettingsAsync()
    {
        var settings = await _repository.GetAll().ToDictionaryAsync(x => x.Key, x => x.Value);

        return settings;
    }

    public async Task<Setting?> GetByIdAsync(int id)
    {
        var setting = await _repository.GetSingleAsync(x => x.Id == id);

        return setting;
    }

    public async Task<Setting?> GetUpdatedSettingAsync(int id)
    {
        var setting = await _repository.GetSingleAsync(x => x.Id == id);

        return setting;
    }

    public async Task<bool?> UpdateAsync(Setting setting, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existedSetting = await _repository.GetSingleAsync(x => x.Id == setting.Id);

        if (existedSetting is null)
            return null;

        var isExist = await _repository.IsExistAsync(x => x.Key.ToLower() == setting.Key.ToLower() && x.Id != setting.Id);

        if (isExist)
        {
            ModelState.AddModelError("Key", "This key is already exist");

            return false;
        }


        existedSetting.Key = setting.Key;
        existedSetting.Value = setting.Value;

        _repository.Update(existedSetting);
        await _repository.SaveAsync();

        return true;

    }
}
