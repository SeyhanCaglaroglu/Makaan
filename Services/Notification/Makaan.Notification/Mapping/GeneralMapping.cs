using AutoMapper;
using Makaan.Notification.Dtos.AccountNotificationDtos;
using Makaan.Notification.Dtos.ApplicationNotificationDtos;
using Makaan.Notification.Dtos.CommentNotificationDtos;
using Makaan.Notification.Entities;

namespace Makaan.Notification.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //CommentNotification
            CreateMap<CommentNotification,ResultCommentNotificationDto>().ReverseMap();
            CreateMap<CommentNotification,CreateCommentNotificationDto>().ReverseMap();

            //ApplicationNotification
            CreateMap<ApplicationNotification, ResultApplicationNotificationDto>().ReverseMap();
            CreateMap<ApplicationNotification, CreateApplicationNotificationDto>().ReverseMap();

            //AccountNotification
            CreateMap<AccountNotification, ResultAccountNotificationDto>().ReverseMap();
            CreateMap<AccountNotification, CreateAccountNotificationDto>().ReverseMap();
        }
    }
}
