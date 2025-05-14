using Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos;
using Makaan.DtoLayer.CatalogDtos.PropertyAgentDtos;
using Makaan.WebUI.Areas.Admin.Models;
using Makaan.WebUI.Services.EstateAgentApplicationServices;
using Makaan.WebUI.Services.PropertyAgentServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EstateAgentApplicationController : Controller
    {
        private readonly IEstateAgentApplicationService _estateAgentApplicationService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        private readonly IPropertyAgentService _propertyAgentService;
        public EstateAgentApplicationController(IEstateAgentApplicationService EstateAgentApplicationService, IUserIdentityService userIdentityService, IUserService userService, IPropertyAgentService propertyAgentService)
        {
            _estateAgentApplicationService = EstateAgentApplicationService;
            _userIdentityService = userIdentityService;
            _userService = userService;
            _propertyAgentService = propertyAgentService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _estateAgentApplicationService.GetAllResultEstateAgentApplicationAsync();

            var Applications = new List<ResultEstateAgentApplicationDto>();

            foreach (var value in values)
            {
                var role = await _userService.GetUserRoleAsync(value.CompanyEmail,value.Password);
                if(role == "Member")
                {
                    Applications.Add(value);
                }
            }

            if (Applications != null)
            {
                return View(Applications);
            }

            return View(new List<ResultEstateAgentApplicationDto>());
        }
        public IActionResult CreateEstateAgentApplication()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEstateAgentApplication(CreateEstateAgentApplicationDto createEstateAgentApplicationDto)
        {
            await _estateAgentApplicationService.CreateEstateAgentApplicationAsync(createEstateAgentApplicationDto);
            return Redirect($"/Admin/EstateAgentApplication/Index");
        }

        public async Task<IActionResult> UpdateEstateAgentApplication(string id)
        {
            var application = await _estateAgentApplicationService.GetByIdEstateAgentApplicationAsync(id);

            return View(application);
        }

        public async Task<IActionResult> DeleteEstateAgentApplication(string id)
        {

            await _estateAgentApplicationService.DeleteEstateAgentApplicationAsync(id);
            return Redirect($"/Admin/EstateAgentApplication/Index");
        }
        //Emlakci Basvurusunun onaylanmasi
        public async Task<IActionResult> ApproveEstateAgentApplication(string id)
        {
            try
            {
                UpdateRoleViewModel updateRoleViewModel = new();

                var application = await _estateAgentApplicationService.GetByIdEstateAgentApplicationAsync(id);

                updateRoleViewModel.Email = application.CompanyEmail;

                await _userIdentityService.UpdateRole(updateRoleViewModel);

                //Olusan Emlakciyi diger tabloya ekleme
                CreatePropertyAgentDto createPropertyAgentDto = new()
                {
                    Title = application.CompanyName,
                    SubTitle = application.AuthorizedPhoneNumber
                };

                await _propertyAgentService.CreatePropertyAgentAsync(createPropertyAgentDto);


                TempData["ConfirmationMessage"] = "Üye Yetkisi Emlakçı Yetkisine Yükseltidi !";
            }
            catch (Exception)
            {
                TempData["ConfirmationMessage"] = "Bir hata oluştu, lütfen sistem yöneticisi ile iletişime geçiniz.";

                return Redirect($"/Admin/EstateAgentApplication/Index");
            }

            return Redirect($"/Admin/EstateAgentApplication/Index");
        }
    }
}
