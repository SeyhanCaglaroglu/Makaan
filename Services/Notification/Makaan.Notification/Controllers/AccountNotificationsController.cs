
using Makaan.Notification.Dtos.AccountNotificationDtos;
using Makaan.Notification.Services.AccountNotificationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountNotificationsController : ControllerBase
    {
        private readonly IAccountNotificationService _accountNotificationService;

        public AccountNotificationsController(IAccountNotificationService accountNotificationService)
        {
            _accountNotificationService = accountNotificationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccountNotification()
        {
            var value = await _accountNotificationService.GetAllResultAccountNotificationAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccountNotification(CreateAccountNotificationDto createAccountNotificationDto)
        {

            await _accountNotificationService.CreateAccountNotificationAsync(createAccountNotificationDto);

            return Ok("Bildirim Başarıyla Eklendi !");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccountNotification(string id)
        {
            await _accountNotificationService.DeleteAccountNotificationAsync(id);
            return Ok("Bildirim Başarıyla Silindi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllAccountNotification()
        {
            await _accountNotificationService.DeleteAllAccountNotificationAsync();
            return Ok("Bildirimler Başarıyla Silindi!");
        }

    }
}
