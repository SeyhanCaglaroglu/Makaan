
using Makaan.Message.Dtos.SenderedMessageDtos;
using Makaan.Message.Services.SenderedMessageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SenderedMessagesController : ControllerBase
    {
        private readonly ISenderedMessageService _senderedMessageService;

        public SenderedMessagesController(ISenderedMessageService senderedMessageService)
        {
            _senderedMessageService = senderedMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSenderedMessage()
        {
            var value = await _senderedMessageService.GetAllResultSenderedMessageAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSenderedMessage(CreateSenderedMessageDto createSenderedMessageDto)
        {

            await _senderedMessageService.CreateSenderedMessageAsync(createSenderedMessageDto);

            return Ok("Bildirim Başarıyla Eklendi !");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdSenderedMessage(string id)
        {
            var value = await _senderedMessageService.GetByIdSenderedMessageAsync(id);
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSenderedMessageByReceiveId(string id)
        {
            var values = await _senderedMessageService.GetAllSenderedMessageByReceiveIdAsync(id);
            return Ok(values);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSenderedMessage(string id)
        {
            await _senderedMessageService.DeleteSenderedMessageAsync(id);
            return Ok("Bildirim Başarıyla Silindi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllSenderedMessage()
        {
            await _senderedMessageService.DeleteAllSenderedMessageAsync();
            return Ok("Bildirimler Başarıyla Silindi!");
        }

    }
}
