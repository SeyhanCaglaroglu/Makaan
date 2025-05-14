using System.ComponentModel.DataAnnotations;

namespace Makaan.WebUI.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
            ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir sembol içermelidir.")]
        public string Password { get; set; }
    }
}
