using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Business.Services.Abstracts;
using Business.ViewModels;

namespace Business.Services.Concretes;


public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(EmailVm vm)
    {
        SmtpClient smtpClient = new SmtpClient(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]))
        {
            Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]),
            EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]),
        };
        MailMessage mailMessage = new MailMessage()
        {
            Subject = vm.subject,
            From = new MailAddress(_configuration["EmailSettings:Email"]),
            IsBodyHtml = bool.Parse(_configuration["EmailSettings:IsHtml"]),
        };
        mailMessage.To.Add(vm.to);

        mailMessage.Body = vm.body;

        smtpClient.Send(mailMessage);
    }
}

