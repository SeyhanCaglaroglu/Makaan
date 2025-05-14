
using Makaan.Notification.Dtos.ApplicationNotificationDtos;
using Makaan.Notification.Services.ApplicationNotificationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationNotificationsController : ControllerBase
    {
        private readonly IApplicationNotificationService _applicationNotificationService;

        public ApplicationNotificationsController(IApplicationNotificationService applicationNotificationService)
        {
            _applicationNotificationService = applicationNotificationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllApplicationNotification()
        {
            var value = await _applicationNotificationService.GetAllResultApplicationNotificationAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplicationNotification(CreateApplicationNotificationDto createApplicationNotificationDto)
        {

            await _applicationNotificationService.CreateApplicationNotificationAsync(createApplicationNotificationDto);

            return Ok("Bildirim Başarıyla Eklendi !");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApplicationNotification(string id)
        {
            await _applicationNotificationService.DeleteApplicationNotificationAsync(id);
            return Ok("Bildirim Başarıyla Silindi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllApplicationNotification()
        {
            await _applicationNotificationService.DeleteAllApplicationNotificationAsync();
            return Ok("Bildirimler Başarıyla Silindi!");
        }

    }
}
