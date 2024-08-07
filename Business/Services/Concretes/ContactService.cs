﻿using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Concretes;

public class ContactService : IContactService
{
    private readonly IEmailService _emailService;

    public ContactService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public bool SendContactMail(ContactVm vm, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid) 
            return false;


        string htmlCode = @$"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>User Contact Info</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }}
        .container {{
            max-width: 600px;
            margin: 50px auto;
            background-color: #33691e;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        h2 {{
            color: #333;
        }}
        .info {{
            margin-bottom: 20px;
        }}
        .info label {{
            font-weight: bold;
        }}
        .info p {{
            margin: 5px 0;
        }}
        .message {{
            margin-bottom: 20px;
        }}
        .message label {{
            font-weight: bold;
        }}
        .message p {{
            margin: 5px 0;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h2>User Contact Information</h2>
        <div class=""info"">
            <label for=""name"">Name:</label>
            <p>{vm.Name}</p>
            <label for=""subject"">Subject:</label>
            <p>{vm.Subject}</p>
            <label for=""email"">Email:</label>
            <p>{vm.Email}</p>
        </div>
        <div class=""message"">
            <label for=""message"">Message:</label>
            <p>{vm.Message}</p>
        </div>
    </div>
</body>
</html>";


        _emailService.SendEmail(new(body: htmlCode, subject: "Contact Info", to: "allupproje@gmail.com"));


        return true;
    }
}
