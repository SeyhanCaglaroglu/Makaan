using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class SignOutController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if(User.Identity?.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync("Cookies");
                return RedirectToAction("Index","Default");
            }

            return RedirectToAction("Index", "Default");
        }
    }
}
