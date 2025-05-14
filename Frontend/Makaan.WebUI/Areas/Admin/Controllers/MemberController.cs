using MailKit.Net.Smtp;
using Makaan.WebUI.Areas.Admin.Hubs;
using Makaan.WebUI.Models;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Xml.Linq;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly EmailSettings _emailSettings;
        private readonly IHubContext<StatisticHub> _statisticHub;
        public MemberController(IUserIdentityService userIdentityService, IOptions<EmailSettings> emailSettings, IHubContext<StatisticHub> statisticHub)
        {
            _userIdentityService = userIdentityService;
            _emailSettings = emailSettings.Value;
            _statisticHub = statisticHub;
        }

        public async Task<IActionResult> Index(string? memberName, int page = 1)
        {
            var members = await _userIdentityService.GetMemberUsers();

            if(!string.IsNullOrEmpty(memberName))
            {
                members = members.Where(x=>x.UserName.Contains(memberName,StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int pageSize = 5;

            // Sayfalama işlemi
            var paginatedMembers = members.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // ViewData ile sayfa bilgilerini gönderiyoruz
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(members.Count / (double)pageSize);
            ViewData["pageSize"] = pageSize;

            return View(paginatedMembers ?? new List<UserDetailViewModel>());
        }

        public async Task<IActionResult> DeleteMember(string email)
        {
            try
            {
                await _userIdentityService.DeleteUser(email);

                //Uye sayisini canli dinleme
                await _statisticHub.Clients.All.SendAsync("ReceiveMemberCountForAdmin", -1);
            }
            catch (Exception)
            {

                TempData["DeleteUserMessage"] = "Kullanıcı Silme İşlemi Başarısız";
                return Redirect("/Admin/Member");
            }

            return Redirect("/Admin/Member");
        }

        public IActionResult SendMailToMember(string email)
        {
            ViewBag.Email = email;
            return View();
        }
        [HttpPost]
        public IActionResult SendMailToMember(string email,string subject, string message)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress($"Makaan", _emailSettings.SenderEmail);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message;

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = subject;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, false);
            smtpClient.Authenticate(_emailSettings.SenderEmail, _emailSettings.Password);
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            TempData["SuccessMailMessage"] = "Mail Bşarıyla İletilmiştir!";

            return Redirect("/Admin/Member");
        }

        public async Task<IActionResult> SendMailAllToMember()
        {
            var members = await _userIdentityService.GetMemberUsers();
            return View(members ?? new List<UserDetailViewModel>());
        }
        [HttpPost]
        public IActionResult SendMailAllToMember(string allEmailAdresses, string subject,string message)
        {
            var emailList = allEmailAdresses.Split(',').Select(x=>x.Trim()).ToList();

            foreach (var email in emailList)
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress($"Makaan", _emailSettings.SenderEmail);
                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = message;

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = subject;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, false);
                smtpClient.Authenticate(_emailSettings.SenderEmail, _emailSettings.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }

            TempData["SuccessMailMessage"] = "Mail Bşarıyla İletilmiştir!";

            return Redirect("/Admin/Member");
        }
    }
}
