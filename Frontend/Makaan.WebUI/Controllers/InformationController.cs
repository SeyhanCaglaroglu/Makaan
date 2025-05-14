using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    [Authorize(Roles = "Member")]
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
