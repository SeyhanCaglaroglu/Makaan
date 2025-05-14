using Makaan.DtoLayer.CommentDtos.MemberCommentDtos;
using Makaan.DtoLayer.NotificationDtos.CommentNotificationDtos;
using Makaan.WebUI.Services.MemberCommentServices;
using Makaan.WebUI.Services.NotificationServices.CommentNotificationServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IMemberCommentService _memberCommentService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly ICommentNotificationService _commentNotificationService;
        public CommentController(IMemberCommentService memberCommentService, IUserIdentityService userIdentityService, ICommentNotificationService commentNotificationService)
        {
            _memberCommentService = memberCommentService;
            _userIdentityService = userIdentityService;
            _commentNotificationService = commentNotificationService;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _memberCommentService.GetAllActiveMemberCommentAsync();
            return View(comments ?? new List<ResultMemberCommentDto>());
        }
        public IActionResult Feature()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Feature(string comment)
        {
            var user = await _userIdentityService.GetUser();

            CreateMemberCommentDto createMemberCommentDto = new();

            createMemberCommentDto.Comment = comment;
            createMemberCommentDto.MemberName = user.UserName;
            createMemberCommentDto.Status = false;

            await _memberCommentService.CreateMemberCommentAsync(createMemberCommentDto);
                        
            TempData["SuccessMessage"] = "Yorumunuz başarıyla eklendi! Değerlendirme sonucunda Sayfaya Eklenicektir !";

            //Eklenen yorumu bildirim olarak ekleme
            CreateCommentNotificationDto createCommentNotificationDto = new();

            createCommentNotificationDto.Content = $"{user.UserName} Kullanıcısı Tarafından Yeni Yorum Yapıldı !";

            await _commentNotificationService.CreateCommentNotificationAsync(createCommentNotificationDto);

            return Redirect("/Comment/Index#dsearch");
        }
    }
}
