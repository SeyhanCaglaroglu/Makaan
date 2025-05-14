using Makaan.DtoLayer.NotificationDtos.CommentNotificationDtos;
using Makaan.WebUI.Areas.Admin.Models;
using Makaan.WebUI.Services.MessageServices.ReceivedMessageServices;
using Makaan.WebUI.Services.MessageServices.SenderedMessageServices;
using Makaan.WebUI.Services.NotificationServices.AccountNotificationServices;
using Makaan.WebUI.Services.NotificationServices.ApplicationNotificationServices;
using Makaan.WebUI.Services.NotificationServices.CommentNotificationServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _HeaderAdminLayoutComponentPartial : ViewComponent
    {
        private readonly ICommentNotificationService _commentNotificationService;
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IApplicationNotificationService _applicationNotificationService;
        private readonly IAccountNotificationService _accountNotificationService;
        private readonly IReceivedMessageService _receivedMessageService;
        private readonly ISenderedMessageService _senderedMessageService;
        public _HeaderAdminLayoutComponentPartial(ICommentNotificationService commentNotificationService, IUserService userService, IUserIdentityService userIdentityService, IApplicationNotificationService applicationNotificationService, IAccountNotificationService accountNotificationService, IReceivedMessageService receivedMessageService, ISenderedMessageService senderedMessageService)
        {
            _commentNotificationService = commentNotificationService;
            _userService = userService;
            _userIdentityService = userIdentityService;
            _applicationNotificationService = applicationNotificationService;
            _accountNotificationService = accountNotificationService;
            _receivedMessageService = receivedMessageService;
            _senderedMessageService = senderedMessageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userIdentityService.GetUser();
            ViewBag.UserEmail = user.Email;
            ViewBag.UserName = user.UserName;

            var userRole = await _userService.GetUserRoleByEmail(user.Email);

            ViewBag.UserRole = userRole;

            if (userRole == "Admin")
            {
                //Yorum Bildirimleri
                var commentNotifications = await _commentNotificationService.GetAllResultCommentNotificationAsync();
                               

                //Basvuru Bildirimleri
                var applicationNotifications = await _applicationNotificationService.GetAllResultApplicationNotificationAsync();
                                
                //Bildirim Sayisi
                ViewBag.NotificationCount = commentNotifications.Count + applicationNotifications.Count;

                //Admine Gelen Mesajlar
                var adminMessages = await _receivedMessageService.GetAllResultReceivedMessageAsync();
                ViewBag.adminMessagesCount = adminMessages.Count;

                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    ApplicationNotifications = applicationNotifications,
                    CommentNotifications = commentNotifications,
                    resultReceivedMessageDtos = adminMessages
                };

                return View(notificationViewModel ?? new NotificationViewModel());
            }

            if (userRole == "EstateAgent")
            {
                //Hesap Bildirimleri
                var accountNotifications = await _accountNotificationService.GetAllResultAccountNotificationAsync();

                //Kullanici mesajlari
                var messages = await _senderedMessageService.GetAllSenderedMessageByReceiveIdAsync(user.Id);
                ViewBag.MessagesCount = messages.Count;

                NotificationViewModel model = new NotificationViewModel
                {
                    AccountNotifications = accountNotifications,
                    resultSenderedMessageDtos = messages
                };
                return View(model ?? new NotificationViewModel());
            }
            else
            {
                //Kullanici mesajlari
                var messages = await _senderedMessageService.GetAllSenderedMessageByReceiveIdAsync(user.Id);
                ViewBag.MessagesCount = messages.Count;

                NotificationViewModel model = new NotificationViewModel
                {
                    resultSenderedMessageDtos = messages
                };
                return View(model ?? new NotificationViewModel());
            }

        }
    }
}
