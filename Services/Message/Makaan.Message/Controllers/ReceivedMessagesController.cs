
using Makaan.Message.Dtos.ReceivedMessageDtos;
using Makaan.Message.Services.ReceivedMessageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceivedMessagesController : ControllerBase
    {
        private readonly IReceivedMessageService _receivedMessageService;

        public ReceivedMessagesController(IReceivedMessageService receivedMessageService)
        {
            _receivedMessageService = receivedMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReceivedMessage()
        {
            var value = await _receivedMessageService.GetAllResultReceivedMessageAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReceivedMessage(CreateReceivedMessageDto createReceivedMessageDto)
        {

            await _receivedMessageService.CreateReceivedMessageAsync(createReceivedMessageDto);

            return Ok("Bildirim Başarıyla Eklendi !");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdReceivedMessage(string id)
        {
            var value = await _receivedMessageService.GetByIdReceivedMessageAsync(id);
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReceivedMessageBySenderId(string id)
        {
            var values = await _receivedMessageService.GetAllReceivedMessageBySenderIdAsync(id);
            return Ok(values);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReceivedMessage(string id)
        {
            await _receivedMessageService.DeleteReceivedMessageAsync(id);
            return Ok("Bildirim Başarıyla Silindi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllReceivedMessage()
        {
            await _receivedMessageService.DeleteAllReceivedMessageAsync();
            return Ok("Bildirimler Başarıyla Silindi!");
        }

    }
}
