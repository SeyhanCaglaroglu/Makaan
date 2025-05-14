using MailKit.Net.Smtp;
using Makaan.DtoLayer.CatalogDtos.ContactDtos;
using Makaan.WebUI.Services.ContactServices;
using Makaan.WebUI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Makaan.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly EmailSettings _emailSettings;
        public ContactController(IContactService contactService, IOptions<EmailSettings> emailSettings)
        {
            _contactService = contactService;
            _emailSettings = emailSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _contactService.GetAllResultContactAsync();

            return View(value ?? new List<ResultContactDto>());
        }
        public IActionResult Feature()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Feature(string name,string email,string subject,string message)
        {

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress($"Makaan ({name +" - "+ email})", _emailSettings.SenderEmail);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", _emailSettings.SenderEmail);
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

            TempData["SuccessMailMessage"] = "Mesajınız Bşarıyla İletilmiştir! En Kısa Zamanda Dönüş Yapılacaktır! Email Kutunuzu Kontrol Etmeyi Unutmayın :)";

            return Redirect("/Contact/Index#dsearch");
        }
    }
}
