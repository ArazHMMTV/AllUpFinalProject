using Business.Services.Abstracts;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Index(ContactVm vm)
        {
            var result = _contactService.SendContactMail(vm,ModelState);

            if(result is false)
                return View(vm);

            return View();

        }
    }
}
