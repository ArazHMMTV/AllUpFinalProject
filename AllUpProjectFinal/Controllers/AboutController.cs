using Microsoft.AspNetCore.Mvc;

namespace AllUpProjectFinal.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
