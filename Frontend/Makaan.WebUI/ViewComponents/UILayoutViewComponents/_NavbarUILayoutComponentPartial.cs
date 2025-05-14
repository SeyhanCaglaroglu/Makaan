using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Makaan.WebUI.Services.PropertyTypeServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        public _NavbarUILayoutComponentPartial(IPropertyTypeService propertyTypeService, IUserIdentityService userIdentityService, IUserService userService)
        {
            _propertyTypeService = propertyTypeService;
            _userIdentityService = userIdentityService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if(User.Identity?.IsAuthenticated == true)
            {
                var user = await _userIdentityService.GetUser();
                var userRole = await _userService.GetUserRoleByEmail(user.Email);

                ViewBag.UserRole = userRole;
            }

            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();
            return View(propertyTypes ?? new List<ResultPropertyTypeDto>());
        }
    }
}
