using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
