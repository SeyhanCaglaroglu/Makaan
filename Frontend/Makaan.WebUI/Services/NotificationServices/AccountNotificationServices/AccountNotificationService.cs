using Makaan.DtoLayer.NotificationDtos.AccountNotificationDtos;
using Makaan.WebUI.Services.NotificationServices.AccountNotificationServices;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.AccountNotificationServices
{
    public class AccountNotificationService : IAccountNotificationService
    {
        private readonly HttpClient _httpClient;
        public AccountNotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAccountNotificationAsync(CreateAccountNotificationDto createAccountNotificationDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateAccountNotificationDto>("AccountNotifications/CreateAccountNotification", createAccountNotificationDto);
        }

        public async Task DeleteAllAccountNotificationAsync()
        {
            await _httpClient.DeleteAsync("AccountNotifications/DeleteAllAccountNotification");
        }

        public async Task DeleteAccountNotificationAsync(string id)
        {
            await _httpClient.DeleteAsync("AccountNotifications/DeleteAccountNotification?id=" + id);
        }

        public async Task<List<ResultAccountNotificationDto>> GetAllResultAccountNotificationAsync()
        {
            var responseMessage = await _httpClient.GetAsync("AccountNotifications/GetAllAccountNotification");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAccountNotificationDto>>();

            return values ?? new List<ResultAccountNotificationDto>();
        }




    }
}
