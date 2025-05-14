using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class AccesDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
