using Makaan.DtoLayer.NotificationDtos.AccountNotificationDtos;
using Makaan.DtoLayer.NotificationDtos.ApplicationNotificationDtos;
using Makaan.DtoLayer.NotificationDtos.CommentNotificationDtos;
using Makaan.WebUI.Areas.Admin.Models;
using Makaan.WebUI.Services.CommentNotificationServices;
using Makaan.WebUI.Services.NotificationServices.AccountNotificationServices;
using Makaan.WebUI.Services.NotificationServices.ApplicationNotificationServices;
using Makaan.WebUI.Services.NotificationServices.CommentNotificationServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,EstateAgent")]
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly ICommentNotificationService _commentNotificationService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        private readonly IApplicationNotificationService _applicationNotificationService;
        private readonly IAccountNotificationService _accountNotificationService;
        public NotificationController(ICommentNotificationService commentNotificationService, IUserIdentityService userIdentityService, IUserService userService, IApplicationNotificationService applicationNotificationService, IAccountNotificationService accountNotificationService)
        {
            _commentNotificationService = commentNotificationService;
            _userIdentityService = userIdentityService;
            _userService = userService;
            _applicationNotificationService = applicationNotificationService;
            _accountNotificationService = accountNotificationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userIdentityService.GetUser();

            var userRole = await _userService.GetUserRoleByEmail(user.Email);


            if (userRole == "Admin")
            {
                //Yorum Bildirimleri
                var commentNotifications = await _commentNotificationService.GetAllResultCommentNotificationAsync();

                //Basvuru Bildirimleri
                var applicationNotifications = await _applicationNotificationService.GetAllResultApplicationNotificationAsync();

                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CommentNotifications = commentNotifications,
                    ApplicationNotifications = applicationNotifications,
                    AccountNotifications = new List<ResultAccountNotificationDto>()
                };

                return View(notificationViewModel ?? new NotificationViewModel());
            }

            if(userRole == "EstateAgent")
            {
                //Hesap Bildirimleri
                var accountNotifications = await _accountNotificationService.GetAllResultAccountNotificationAsync();

                NotificationViewModel model = new NotificationViewModel
                {
                    AccountNotifications = accountNotifications,
                    ApplicationNotifications = new List<ResultApplicationNotificationDto>(),
                    CommentNotifications = new List<ResultCommentNotificationDto>()

                };
                return View(model ?? new NotificationViewModel());
            }

            return View(new NotificationViewModel());
        }

        public async Task<IActionResult> DeleteCommentNotification(string id)
        {
            await _commentNotificationService.DeleteCommentNotificationAsync(id);
            return Redirect("/Admin/Notification/Index");
        }
        public async Task<IActionResult> DeleteAllCommentNotification()
        {
            await _commentNotificationService.DeleteAllCommentNotificationAsync();
            return Redirect("/Admin/Notification/Index");
        }

        public async Task<IActionResult> DeleteApplicationNotification(string id)
        {
            await _applicationNotificationService.DeleteApplicationNotificationAsync(id);
            return Redirect("/Admin/Notification/Index");
        }
        public async Task<IActionResult> DeleteAllApplicationNotification()
        {
            await _applicationNotificationService.DeleteAllApplicationNotificationAsync();
            return Redirect("/Admin/Notification/Index");
        }

        public async Task<IActionResult> DeleteAccountNotification(string id)
        {
            await _accountNotificationService.DeleteAccountNotificationAsync(id);
            return Redirect("/Admin/Notification/Index");
        }
    }
}
