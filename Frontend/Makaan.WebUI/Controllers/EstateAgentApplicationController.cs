using Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos;
using Makaan.DtoLayer.NotificationDtos.ApplicationNotificationDtos;
using Makaan.WebUI.Models;
using Makaan.WebUI.Services.EstateAgentApplicationServices;
using Makaan.WebUI.Services.NotificationServices.ApplicationNotificationServices;
using Makaan.WebUI.Services.SignUpServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class EstateAgentApplicationController : Controller
    {
        private readonly ISignUpService _signUpService;
        private readonly IEstateAgentApplicationService _estateAgentApplicationService;
        private readonly IApplicationNotificationService _applicationNotificationService;
        public EstateAgentApplicationController(ISignUpService signUpService, IEstateAgentApplicationService estateAgentApplicationService, IApplicationNotificationService applicationNotificationService)
        {
            _signUpService = signUpService;
            _estateAgentApplicationService = estateAgentApplicationService;
            _applicationNotificationService = applicationNotificationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateEstateAgentApplicationDto createEstateAgentApplicationDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createEstateAgentApplicationDto);
            }

            SaveUserViewModel saveUserViewModel = new SaveUserViewModel
            {
                UserName = createEstateAgentApplicationDto.AuthorizedNameSurname,
                Email = createEstateAgentApplicationDto.CompanyEmail,
                City = createEstateAgentApplicationDto.City,
                Password = createEstateAgentApplicationDto.Password,
                PasswordAgain = createEstateAgentApplicationDto.PasswordAgain
            };

            try
            {
                await _signUpService.CreateUserAsync(saveUserViewModel);

                await _estateAgentApplicationService.CreateEstateAgentApplicationAsync(createEstateAgentApplicationDto);

                TempData["ApplicationMessage"] = "Hesabınız Normal Kullanıcı Düzeyinde Oluşturuldu, Gerekli İncelemelerden Sonra, Hesabınız Emlakçı Yetkisine Yükseltilecek, Bildirim Kutunuzu Kontrol Etmeyi Unutmayın :)";

                //Basvuruyu bildirim olarak ekleme
                CreateApplicationNotificationDto createApplicationNotificationDto = new();

                createApplicationNotificationDto.Content = $"{createEstateAgentApplicationDto.CompanyName} Şirketi tarafından Emlakçılık Başvurusu Yapıldı !";

                await _applicationNotificationService.CreateApplicationNotificationAsync(createApplicationNotificationDto);
            }
            catch (Exception)
            {
                TempData["ApplicationMessage"] = "Bir hata oluştu, lütfen sistem yöneticisi ile iletişime geçiniz.";
                return RedirectToAction("Login","Account");
            }
            

            return RedirectToAction("Login", "Account");
        }
    }
}
