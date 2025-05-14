using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult _UILayout()
        {
            return View();
        }
    }
}
