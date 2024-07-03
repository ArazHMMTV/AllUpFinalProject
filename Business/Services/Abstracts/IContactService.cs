using Business.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Abstracts;

public interface IContactService
{
    bool SendContactMail(ContactVm vm, ModelStateDictionary ModelState);
}
