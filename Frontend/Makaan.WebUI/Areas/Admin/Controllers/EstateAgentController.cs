using MailKit.Net.Smtp;
using Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos;
using Makaan.WebUI.Areas.Admin.Hubs;
using Makaan.WebUI.Models;
using Makaan.WebUI.Services.EstateAgentApplicationServices;
using Makaan.WebUI.Services.PropertyAgentServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Makaan.WebUI.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EstateAgentController : Controller
    {
        private readonly IEstateAgentApplicationService _estateAgentApplicationService;
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IPropertyAgentService _propertyAgentService;
        private readonly EmailSettings _emailSettings;
        private readonly IHubContext<StatisticHub> _statisticHub;
        public EstateAgentController(IEstateAgentApplicationService estateAgentApplicationService, IUserService userService, IUserIdentityService userIdentityService, IPropertyAgentService propertyAgentService, IOptions<EmailSettings> emailSettings, IHubContext<StatisticHub> statisticHub)
        {
            _estateAgentApplicationService = estateAgentApplicationService;
            _userService = userService;
            _userIdentityService = userIdentityService;
            _propertyAgentService = propertyAgentService;
            _emailSettings = emailSettings.Value;
            _statisticHub = statisticHub;
        }

        public async Task<IActionResult> Index(string? companyName, int page = 1)
        {

            var values = await _estateAgentApplicationService.GetAllResultEstateAgentApplicationAsync();

            if (!string.IsNullOrEmpty(companyName))
            {
                values = values.Where(x=>x.CompanyName.Contains(companyName,StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var Applications = new List<ResultEstateAgentApplicationDto>();

            foreach (var value in values)
            {
                var role = await _userService.GetUserRoleAsync(value.CompanyEmail, value.Password);
                if (role == "EstateAgent")
                {
                    Applications.Add(value);
                }
            }

            int pageSize = 5;

            // Sayfalama işlemi
            var paginatedEstateAgents = Applications.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // ViewData ile sayfa bilgilerini gönderiyoruz
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(Applications.Count / (double)pageSize);
            ViewData["pageSize"] = pageSize;

            if (values != null)
            {
                return View(paginatedEstateAgents);
            }

            return View(new List<ResultEstateAgentApplicationDto>());
        }
        public async Task<IActionResult> DetailEstateAgent(string id)
        {
            var application = await _estateAgentApplicationService.GetByIdEstateAgentApplicationAsync(id);
            return View(application);
        }

        public async Task<IActionResult> DeleteEstateAgent(string id, string email)
        {
            try
            {
                await _estateAgentApplicationService.DeleteEstateAgentApplicationAsync(id);

                await _userIdentityService.DeleteUser(email);

                //Emlakci sayisini canli dinleme
                await _statisticHub.Clients.All.SendAsync("ReceiveEstateAgentCountForAdmin", -1);
            }
            catch (Exception)
            {

                throw;
            }
            
            return Redirect($"/Admin/EstateAgent/Index");
        }

        public IActionResult CreateEstateAgent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEstateAgent(CreateEstateAgentApplicationDto createEstateAgentApplicationDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createEstateAgentApplicationDto);
            }

            SaveUserViewModel saveUserViewModel = new SaveUserViewModel
            {
                UserName = createEstateAgentApplicationDto.AuthorizedNameSurname,
                Email = createEstateAgentApplicationDto.CompanyEmail,
                City = createEstateAgentApplicationDto.City,
                Password = createEstateAgentApplicationDto.Password,
                PasswordAgain = createEstateAgentApplicationDto.PasswordAgain
            };

            try
            {
                await _userIdentityService.CreateEstateAgent(saveUserViewModel);

                await _estateAgentApplicationService.CreateEstateAgentApplicationAsync(createEstateAgentApplicationDto);

                TempData["ApplicationMessage"] = "Emlakçı Hesabı Başarı ile oluşturuldu !";

                //Emlakci sayisini canli dinleme
                await _statisticHub.Clients.All.SendAsync("ReceiveEstateAgentCountForAdmin", 1);
            }
            catch (Exception ex)
            {
                TempData["ApplicationMessage"] = "Bir hata oluştu, lütfen sistem yöneticisi ile iletişime geçiniz." + ex.Message;
                return Redirect($"/Admin/EstateAgent/Index");
            }

            return Redirect($"/Admin/EstateAgent/Index");
        }

        public IActionResult SendMailToEstateAgent(string email)
        {
            ViewBag.Email = email;

            return View();
        }
        [HttpPost]
        public IActionResult SendMailToEstateAgent(string email, string subject, string message)
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

            ViewData["SuccessMailMessage"] = "Mail Bşarıyla İletilmiştir!";

            return Redirect("/Admin/EstateAgent");
        }

        public async Task<IActionResult> SendMailAllToEstateAgent()
        {
            var estateAgentUsers = await _userIdentityService.GetEstateAgentUsers();
            return View(estateAgentUsers ?? new List<UserDetailViewModel>());
        }
        [HttpPost]
        public IActionResult SendMailAllToEstateAgent(string allEmailAdresses, string subject, string message)
        {
            var emailList = allEmailAdresses.Split(',').Select(x => x.Trim()).ToList();

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

            ViewData["SuccessMailMessage"] = "Mail Bşarıyla İletilmiştir!";

            return Redirect("/Admin/EstateAgent");
        }
    }
}
