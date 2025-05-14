using Makaan.DtoLayer.NotificationDtos.ApplicationNotificationDtos;
using Makaan.WebUI.Services.NotificationServices.ApplicationNotificationServices;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.ApplicationNotificationServices
{
    public class ApplicationNotificationService : IApplicationNotificationService
    {
        private readonly HttpClient _httpClient;
        public ApplicationNotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateApplicationNotificationAsync(CreateApplicationNotificationDto createApplicationNotificationDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateApplicationNotificationDto>("ApplicationNotifications/CreateApplicationNotification", createApplicationNotificationDto);
        }
               
        public async Task DeleteAllApplicationNotificationAsync()
        {
            await _httpClient.DeleteAsync("ApplicationNotifications/DeleteAllApplicationNotification");
        }

        public async Task DeleteApplicationNotificationAsync(string id)
        {
            await _httpClient.DeleteAsync("ApplicationNotifications/DeleteApplicationNotification?id=" + id);
        }

        public async Task<List<ResultApplicationNotificationDto>> GetAllResultApplicationNotificationAsync()
        {
            var responseMessage = await _httpClient.GetAsync("ApplicationNotifications/GetAllApplicationNotification");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultApplicationNotificationDto>>();

            return values ?? new List<ResultApplicationNotificationDto>();
        }
              
                
    }
}
