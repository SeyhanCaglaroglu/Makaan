using Makaan.Notification.Dtos.ApplicationNotificationDtos;

namespace Makaan.Notification.Services.ApplicationNotificationServices
{
    public interface IApplicationNotificationService
    {
        Task<List<ResultApplicationNotificationDto>> GetAllResultApplicationNotificationAsync();
        Task CreateApplicationNotificationAsync(CreateApplicationNotificationDto createApplicationNotificationDto);
        Task DeleteApplicationNotificationAsync(string id);
        Task DeleteAllApplicationNotificationAsync();
    }
}
