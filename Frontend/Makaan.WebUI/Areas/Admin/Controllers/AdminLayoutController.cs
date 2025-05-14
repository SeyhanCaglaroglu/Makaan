using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminLayoutController : Controller
    {
      
        public IActionResult _Layout()
        {
            return View();
        }
    }
}
