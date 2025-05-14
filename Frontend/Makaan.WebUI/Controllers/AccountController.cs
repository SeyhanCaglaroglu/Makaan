using Makaan.WebUI.Models;
using Makaan.WebUI.Services.SignInServices;
using Makaan.WebUI.Services.SignUpServices;
using Makaan.WebUI.Services.UserServices;
using Makaan.WebUI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Makaan.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISignUpService _signUpService;
        private readonly ISignInService _signInService;
        private readonly IUserService _userService;
        private readonly EmailSettings _emailSettings;
        public AccountController(ISignUpService signUpService, ISignInService signInService, IUserService userService, IOptions<EmailSettings> emailSettings)
        {
            _signUpService = signUpService;
            _signInService = signInService;
            _userService = userService;
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("/Default/Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel signInViewModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(signInViewModel);
            }

            var result = await _signInService.SignInAsync(signInViewModel);

            if (result)
            {
                // Eğer ReturnUrl geçerli bir yerel URL ise, kullanıcıyı oraya yönlendiriyoruz
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                // Geçerli bir ReturnUrl yoksa, varsayılan sayfaya yönlendiriyoruz
                return RedirectToAction("Index", "Default");
            }

            // Giriş başarısız ise tekrar login sayfasına dönülür
            ViewBag.Result = result;

            return View();
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SaveUserViewModel saveUserViewModel)
        {//admindeki linkleri kontrol et, signout u kontrol et,
            if (!ModelState.IsValid)
            {
                return View(saveUserViewModel);
            }

            try
            {
                await _signUpService.CreateUserAsync(saveUserViewModel);
                TempData["SuccessCreateAccountMessage"] = "Hesabınız Başarı İle Oluşturuldu !";
            }
            catch (Exception)
            {
                TempData["FailCreateAccountMessage"] = "Bir Hata Oluştu !";
                return View(saveUserViewModel);
            }
            

            return RedirectToAction("Login","Account");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var userChecked = await _userService.GetUserRoleByEmail(resetPasswordViewModel.Email);

            if(!string.IsNullOrEmpty(userChecked))
            {
                var newPassword = GenerateRandomPassword();

                await _userService.UpdatePassword(resetPasswordViewModel.Email, newPassword);

                TempData["SuccessResetPasswordMessage"] = "Şifreniz Başarı ile Sıfırlandı !";

                string senderedEmail = _emailSettings.SenderEmail; // E-posta adresiniz
                string senderedPassword = _emailSettings.Password; // E-posta şifreniz

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderedEmail, senderedPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderedEmail),
                    Subject = "Yeni Şifreniz",
                    Body = $"Yeni Şifreniz: {newPassword}",
                    IsBodyHtml = false // HTML içermiyor
                };

                mailMessage.To.Add(resetPasswordViewModel.Email); // Alıcının e-posta adresi

                smtpClient.Send(mailMessage);

                return RedirectToAction("Login", "Account");
            }

            TempData["FailedResetPasswordMessage"] = "Email Adresi Bulunamadı !";
            return View(resetPasswordViewModel);

        }

        private string GenerateRandomPassword()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();

            // Minimum gereklilikleri sağla
            string password =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[random.Next(26)].ToString() + // 1 büyük harf
                "abcdefghijklmnopqrstuvwxyz"[random.Next(26)] +           // 1 küçük harf
                "0123456789"[random.Next(10)] +                          // 1 sayı
                "!@#$%^&*()"[random.Next(10)];                           // 1 sembol

            // Kalan karakterleri ekle
            password += new string(Enumerable.Repeat(characters, 4) // Toplam 8 karakter
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Karakterleri karıştır
            return new string(password.OrderBy(_ => random.Next()).ToArray());
        }

    }
}
