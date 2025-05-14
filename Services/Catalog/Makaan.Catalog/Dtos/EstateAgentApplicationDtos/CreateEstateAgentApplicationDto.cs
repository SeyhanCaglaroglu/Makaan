namespace Makaan.Catalog.Dtos.EstateAgentApplicationDtos
{
    public class CreateEstateAgentApplicationDto
    {
        public string CompanyName { get; set; }
        public string AuthorizedNameSurname { get; set; }
        public string AuthorizedPhoneNumber { get; set; }
        public string CompanyEmail { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
    }
}
