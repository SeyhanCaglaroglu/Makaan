using Makaan.DtoLayer.NotificationDtos.AccountNotificationDtos;

namespace Makaan.WebUI.Services.NotificationServices.AccountNotificationServices
{
    public interface IAccountNotificationService
    {
        Task<List<ResultAccountNotificationDto>> GetAllResultAccountNotificationAsync();
        Task CreateAccountNotificationAsync(CreateAccountNotificationDto createAccountNotificationDto);
        Task DeleteAccountNotificationAsync(string id);
        Task DeleteAllAccountNotificationAsync();
    }
}
