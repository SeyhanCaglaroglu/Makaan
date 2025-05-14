using Makaan.WebUI.Models;

namespace Makaan.WebUI.Services.SignUpServices
{
    public class SignUpService : ISignUpService
    {
        private readonly HttpClient _httpClient;

        public SignUpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateUserAsync(SaveUserViewModel saveUserViewModel)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync<SaveUserViewModel>("/api/users/signup", saveUserViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı Oluşturulamadı !", ex);
            }
            

            
        }
    }
}
