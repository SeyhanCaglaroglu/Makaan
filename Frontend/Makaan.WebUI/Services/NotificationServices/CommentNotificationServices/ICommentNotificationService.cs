using Makaan.DtoLayer.NotificationDtos.CommentNotificationDtos;

namespace Makaan.WebUI.Services.NotificationServices.CommentNotificationServices
{
    public interface ICommentNotificationService
    {
        Task<List<ResultCommentNotificationDto>> GetAllResultCommentNotificationAsync();
        Task CreateCommentNotificationAsync(CreateCommentNotificationDto createCommentNotificationDto);
        Task DeleteCommentNotificationAsync(string id);
        Task DeleteAllCommentNotificationAsync();
    }
}
