using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _SidebarAdminLayoutComponentPartial : ViewComponent
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;

        public _SidebarAdminLayoutComponentPartial(IUserIdentityService userIdentityService, IUserService userService)
        {
            _userIdentityService = userIdentityService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userIdentityService.GetUser();

            var userRole = await _userService.GetUserRoleByEmail(user.Email);

            ViewBag.UserRole = userRole;

            return View();
        }
    }
}
