using Business.ViewModels;
using Core.Models;

namespace Business.Services.Abstracts;

public interface ILayoutService
{
    Task<Dictionary<string, string>> GetSettingsAsync();
    Task<List<Category>> GetAllCategoriesAsync();
    Task<List<Product>> SearchAsync(SearchVm vm);
        
}
