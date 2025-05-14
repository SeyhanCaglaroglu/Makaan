using Makaan.DtoLayer.NotificationDtos.ApplicationNotificationDtos;

namespace Makaan.WebUI.Services.NotificationServices.ApplicationNotificationServices
{
    public interface IApplicationNotificationService
    {
        Task<List<ResultApplicationNotificationDto>> GetAllResultApplicationNotificationAsync();
        Task CreateApplicationNotificationAsync(CreateApplicationNotificationDto createApplicationNotificationDto);
        Task DeleteApplicationNotificationAsync(string id);
        Task DeleteAllApplicationNotificationAsync();
    }
}
