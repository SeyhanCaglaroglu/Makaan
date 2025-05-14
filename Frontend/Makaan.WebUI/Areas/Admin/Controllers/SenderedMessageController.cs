using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;
using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;
using Makaan.WebUI.Areas.Admin.Models;
using Makaan.WebUI.Services.MessageServices.ReceivedMessageServices;
using Makaan.WebUI.Services.MessageServices.SenderedMessageServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Member,EstateAgent")]
    [Area("Admin")]
    public class SenderedMessageController : Controller
    {
        private readonly ISenderedMessageService _senderedMessageService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        private readonly IReceivedMessageService _receivedMessageService;
        public SenderedMessageController(ISenderedMessageService senderedMessageService, IUserIdentityService userIdentityService, IUserService userService, IReceivedMessageService receivedMessageService)
        {
            _senderedMessageService = senderedMessageService;
            _userIdentityService = userIdentityService;
            _userService = userService;
            _receivedMessageService = receivedMessageService;
        }

        public async Task<IActionResult> Index()
        {

            var user = await _userIdentityService.GetUser();

            var userRole = await _userService.GetUserRoleByEmail(user.Email);

            var users = await _userIdentityService.GetAllUsers();

            var messages = await _senderedMessageService.GetAllSenderedMessageByReceiveIdAsync(user.Id);

            SenderedMessagesAndUsersViewModel model = new()
            {
                resultSenderedMessageDtos = messages,
                userDetailViewModels = users
            };
            return View(model ?? new SenderedMessagesAndUsersViewModel());


        }
        public async Task<IActionResult> CreateSenderedMessage()
        {
            var user = await _userIdentityService.GetUser();
            ViewBag.UserId = user.Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSenderedMessage(CreateReceivedMessageDto createReceivedMessageDto)
        {
            await _receivedMessageService.CreateReceivedMessageAsync(createReceivedMessageDto);
            TempData["SuccessSendMessage"] = "Mesajınız Başarı ile Gönderilmiştir !";
            return Redirect("/Admin/SenderedMessage/Index");
        }

        public async Task<IActionResult> GetByIdSenderedMessage(string id)
        {
            var message = await _senderedMessageService.GetByIdSenderedMessageAsync(id);
            return View(message);
        }

        public async Task<IActionResult> DeleteSenderedMessage(string id)
        {
            await _senderedMessageService.DeleteSenderedMessageAsync(id);
            return Redirect("/Admin/SenderedMessage/Index");
        }
        public async Task<IActionResult> DeleteAllSenderedMessage()
        {
            await _senderedMessageService.DeleteAllSenderedMessageAsync();
            return Redirect("/Admin/SenderedMessage/Index");
        }
    }
}
