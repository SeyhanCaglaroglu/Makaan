using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        public ProfileController(IUserIdentityService userIdentityService, IUserService userService)
        {
            _userIdentityService = userIdentityService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userIdentityService.GetUser();

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string email)
        {
            await _userIdentityService.DeleteUser(email);

            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("Index","Default");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string newPassword,string newPasswordAgain)
        {
            var user = await _userIdentityService.GetUser();

            if(newPassword == newPasswordAgain)
            {
                await _userService.UpdatePassword(user.Email, newPassword);

                ViewData["SuccessResetPasswordMessage"] = "Şifreniz Başarı ile Değiştirildi !";
                return Redirect("/Admin/Profile/Index");

            }

            ViewData["FailedResetPasswordMessage"] = "Şifreler Uyuşmuyor !";

            return View();
        }
    }
}
