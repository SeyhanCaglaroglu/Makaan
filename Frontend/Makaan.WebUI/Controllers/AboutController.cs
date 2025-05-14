using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
