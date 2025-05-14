
using Makaan.Notification.Dtos.CommentNotificationDtos;
using Makaan.Notification.Services.CommentNotificationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentNotificationsController : ControllerBase
    {
        private readonly ICommentNotificationService _commentNotificationService;

        public CommentNotificationsController(ICommentNotificationService CommentNotificationService)
        {
            _commentNotificationService = CommentNotificationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCommentNotification()
        {
            var value = await _commentNotificationService.GetAllResultCommentNotificationAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCommentNotification(CreateCommentNotificationDto createCommentNotificationDto)
        {

            await _commentNotificationService.CreateCommentNotificationAsync(createCommentNotificationDto);

            return Ok("Bildirim Başarıyla Eklendi !");
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentNotification(string id)
        {
            await _commentNotificationService.DeleteCommentNotificationAsync(id);
            return Ok("Bildirim Başarıyla Silindi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllCommentNotification()
        {
            await _commentNotificationService.DeleteAllCommentNotificationAsync();
            return Ok("Bildirimler Başarıyla Silindi!");
        }
        
    }
}
