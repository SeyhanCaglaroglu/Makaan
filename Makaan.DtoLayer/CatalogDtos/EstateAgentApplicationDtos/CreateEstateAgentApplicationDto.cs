using System.ComponentModel.DataAnnotations;

namespace Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos
{
    public class CreateEstateAgentApplicationDto
    {

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Yetkili Adı Soyadı zorunludur.")]
        public string AuthorizedNameSurname { get; set; }

        [Required(ErrorMessage = "Yetkili Telefon Numarası zorunludur.")]
        public string AuthorizedPhoneNumber { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string CompanyEmail { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
            ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir sembol içermelidir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string PasswordAgain { get; set; }

        [Required(ErrorMessage = "Şehir bilgisi zorunludur.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Adres bilgisi zorunludur.")]
        public string Location { get; set; }
    }
}
