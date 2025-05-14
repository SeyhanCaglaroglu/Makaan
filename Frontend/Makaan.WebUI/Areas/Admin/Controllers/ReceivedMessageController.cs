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
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ReceivedMessageController : Controller
    {
        private readonly IReceivedMessageService _receivedMessageService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        public readonly ISenderedMessageService _senderedMessageService;
        public ReceivedMessageController(IReceivedMessageService receivedMessageService, IUserIdentityService userIdentityService, IUserService userService, ISenderedMessageService senderedMessageService)
        {
            _receivedMessageService = receivedMessageService;
            _userIdentityService = userIdentityService;
            _userService = userService;
            _senderedMessageService = senderedMessageService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userIdentityService.GetUser();
            var userRole = await _userService.GetUserRoleByEmail(user.Email);
            ViewBag.UserRole = userRole;

            var users = await _userIdentityService.GetAllUsers();
            List<ResultReceivedMessageDto> receivedMessages;

            if (userRole == "Admin")
            {
                receivedMessages = await _receivedMessageService.GetAllResultReceivedMessageAsync();
            }
            else
            {
                receivedMessages = await _receivedMessageService.GetAllReceivedMessageBySenderIdAsync(user.Id);
            }

            ReceiveMessagesAndUsersViewModel receiveMessagesAndUsersViewModel = new()
            {
                resultReceivedMessageDtos = receivedMessages,
                userDetailViewModels = users
            };

            return View(receiveMessagesAndUsersViewModel);
        }

        //public IActionResult CreateReceivedMessage()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateReceivedMessage(CreateReceivedMessageDto createReceivedMessageDto)
        //{
        //    await _receivedMessageService.CreateReceivedMessageAsync(createReceivedMessageDto);
        //    return Redirect("/Admin/ReceivedMessage/Index");
        //}
        
        public async Task<IActionResult> GetByIdReceivedMessage(string id)
        {
            var user = await _userIdentityService.GetUser();
            var userRole = await _userService.GetUserRoleByEmail(user.Email);
            ViewBag.UserRole = userRole;

            var users = await _userIdentityService.GetAllUsers();

            var receivedMessage = await _receivedMessageService.GetByIdReceivedMessageAsync(id);

            

            ReceiveMessageAndUsersViewModel receiveMessageAndUsersViewModel = new()
            {
                getByIdReceivedMessageDto = receivedMessage,
                userDetailViewModels = users
            };

            return View(receiveMessageAndUsersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ReplyToMessage(CreateSenderedMessageDto createSenderedMessageDto, string ReceiveMessageID)
        {
            await _senderedMessageService.CreateSenderedMessageAsync(createSenderedMessageDto);

            TempData["SuccessReplyToMessage"] = "Cevabınız Başarı İle İletimiştir !";

            await _receivedMessageService.DeleteReceivedMessageAsync(ReceiveMessageID);

            return Redirect("/Admin/ReceivedMessage/Index");
        }


        public async Task<IActionResult> DeleteReceivedMessage(string id)
        {
            await _receivedMessageService.DeleteReceivedMessageAsync(id);
            return Redirect("/Admin/ReceivedMessage/Index");
        }
        public async Task<IActionResult> DeleteAllReceivedMessage()
        {
            await _receivedMessageService.DeleteAllReceivedMessageAsync();
            return Redirect("/Admin/ReceivedMessage/Index");
        }
    }
}
