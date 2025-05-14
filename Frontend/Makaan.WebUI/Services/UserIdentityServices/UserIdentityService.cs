using Makaan.WebUI.Areas.Admin.Models;
using Makaan.WebUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Makaan.WebUI.Services.UserIdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;

        public UserIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEstateAgent(SaveUserViewModel saveUserViewModel)
        {
            // POST isteği gönderiyoruz
            var responseMessage = await _httpClient.PostAsJsonAsync<SaveUserViewModel>("/api/Users/CreateEstateAgent", saveUserViewModel);

            // API çağrısı başarılı değilse hata mesajını döndürüyoruz
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }
        }

        public async Task DeleteUser(string email)
        {
            // API'ye DELETE isteği gönderiyoruz
            var responseMessage = await _httpClient.DeleteAsync($"/api/Users/DeleteUser?email={email}");

            // API çağrısı başarılı değilse hata fırlatıyoruz
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }
        }

        public async Task<List<UserDetailViewModel>> GetAllUsers()
        {
            var responseMessage = await _httpClient.GetAsync("/api/Users/GetAllUsers");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<List<UserDetailViewModel>>();

            return value ?? new List<UserDetailViewModel>();
        }

        public async Task<List<UserDetailViewModel>> GetEstateAgentUsers()
        {
            var responseMessage = await _httpClient.GetAsync("/api/Users/GetEstateAgentUsers");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<List<UserDetailViewModel>>();

            return value ?? new List<UserDetailViewModel>();
        }

        public async Task<List<UserDetailViewModel>> GetMemberUsers()
        {
            var responseMessage = await _httpClient.GetAsync("/api/Users/GetMemberUsers");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<List<UserDetailViewModel>>();

            return value ?? new List<UserDetailViewModel>();
        }

        public async Task<UserDetailViewModel> GetUser()
        {
            var responseMessage = await _httpClient.GetAsync($"/api/Users/GetUser");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<UserDetailViewModel>();

            return value ?? new UserDetailViewModel();

        }

        
        public async Task UpdateRole(UpdateRoleViewModel updateRoleViewModel)
        {

            // POST isteği gönderiyoruz
            var responseMessage = await _httpClient.PostAsJsonAsync<UpdateRoleViewModel>("/api/Users/UpdateRole", updateRoleViewModel);

            // API çağrısı başarılı değilse hata mesajını döndürüyoruz
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }
        }
    }
}
