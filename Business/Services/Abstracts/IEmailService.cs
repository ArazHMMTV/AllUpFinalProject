using Business.Services.Concretes;
using Business.ViewModels;

namespace Business.Services.Abstracts;

public interface IEmailService
{
    void SendEmail(EmailVm vm);
}
