using Business.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface IAccountService
{
    Task<bool> LoginAsync(LoginVm vm,ModelStateDictionary ModelState);
    Task<bool> LogoutAsync();
    Task<bool> RegisterAsync(RegisterVm vm,ModelStateDictionary ModelState);
}
