using Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface ISettingService
{
    Task<List<Setting>> GetAllAsync();
    Task<Dictionary<string, string>> GetAllSettingsAsync();
    Task<Setting?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Setting setting,ModelStateDictionary ModelState);
    Task<bool?> UpdateAsync(Setting setting, ModelStateDictionary ModelState);
    Task<Setting?> GetUpdatedSettingAsync(int id);
    Task<bool> DeleteAsync(int id);

}
