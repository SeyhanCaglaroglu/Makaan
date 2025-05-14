using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;
using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;
using Makaan.DtoLayer.NotificationDtos.AccountNotificationDtos;
using Makaan.DtoLayer.NotificationDtos.ApplicationNotificationDtos;
using Makaan.DtoLayer.NotificationDtos.CommentNotificationDtos;

namespace Makaan.WebUI.Areas.Admin.Models
{
    public class NotificationViewModel
    {
        public List<ResultCommentNotificationDto> CommentNotifications { get; set; }
        public List<ResultApplicationNotificationDto> ApplicationNotifications { get; set; }
        public List<ResultAccountNotificationDto> AccountNotifications { get; set; }

        //Message
        public List<ResultSenderedMessageDto> resultSenderedMessageDtos { get; set; }
        public List<ResultReceivedMessageDto> resultReceivedMessageDtos { get; set; }
    }
}
