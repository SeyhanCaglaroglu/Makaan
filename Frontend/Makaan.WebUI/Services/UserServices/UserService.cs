
using Makaan.WebUI.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Makaan.WebUI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    

        public async Task<string> GetUserRoleAsync(string Email, string Password)
        {
            var responseMessage = await _httpClient.GetAsync($"/api/Users/GetUserRole?Email={Email}&Password={Password}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadAsStringAsync();
            return value ?? "";
        }

        public async Task<string> GetUserRoleByEmail(string Email)
        {
            var responseMessage = await _httpClient.GetAsync($"/api/Users/GetUserRoleByEmail?Email={Email}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadAsStringAsync();
            return value ?? "";
        }

        public async Task UpdatePassword(string email, string newPassword)
        {


            var responseMessage = await _httpClient.PostAsync($"/api/Users/UpdatePassword?email={email}&newPassword={newPassword}", null);

            // API çağrısı başarılı değilse hata mesajını döndürüyoruz
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }
        }
    }
}
