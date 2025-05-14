using Makaan.DtoLayer.NotificationDtos.CommentNotificationDtos;
using Makaan.WebUI.Services.NotificationServices.CommentNotificationServices;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.CommentNotificationServices
{
    public class CommentNotificationService : ICommentNotificationService
    {
        private readonly HttpClient _httpClient;
        public CommentNotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCommentNotificationAsync(CreateCommentNotificationDto createCommentNotificationDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateCommentNotificationDto>("CommentNotifications/CreateCommentNotification", createCommentNotificationDto);
        }

        public async Task DeleteAllCommentNotificationAsync()
        {
            await _httpClient.DeleteAsync("CommentNotifications/DeleteAllCommentNotification");
        }

        public async Task DeleteCommentNotificationAsync(string id)
        {
            await _httpClient.DeleteAsync("CommentNotifications/DeleteCommentNotification?id=" + id);
        }

        public async Task<List<ResultCommentNotificationDto>> GetAllResultCommentNotificationAsync()
        {
            var responseMessage = await _httpClient.GetAsync("CommentNotifications/GetAllCommentNotification");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentNotificationDto>>();

            return values ?? new List<ResultCommentNotificationDto>();
        }

        


    }
}
