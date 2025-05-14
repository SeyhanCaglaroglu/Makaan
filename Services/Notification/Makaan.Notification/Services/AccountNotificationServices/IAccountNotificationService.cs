using Makaan.Notification.Dtos.AccountNotificationDtos;

namespace Makaan.Notification.Services.AccountNotificationServices
{
    public interface IAccountNotificationService
    {
        Task<List<ResultAccountNotificationDto>> GetAllResultAccountNotificationAsync();
        Task CreateAccountNotificationAsync(CreateAccountNotificationDto createAccountNotificationDto);
        Task DeleteAccountNotificationAsync(string id);
        Task DeleteAllAccountNotificationAsync();
    }
}
