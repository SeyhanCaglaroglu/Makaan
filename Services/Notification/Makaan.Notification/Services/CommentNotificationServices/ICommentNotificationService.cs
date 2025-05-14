using Makaan.Notification.Dtos.CommentNotificationDtos;

namespace Makaan.Notification.Services.CommentNotificationServices
{
    public interface ICommentNotificationService
    {
        Task<List<ResultCommentNotificationDto>> GetAllResultCommentNotificationAsync();
        Task CreateCommentNotificationAsync(CreateCommentNotificationDto createCommentNotificationDto);
        Task DeleteCommentNotificationAsync(string id);
        Task DeleteAllCommentNotificationAsync();
    }
}
