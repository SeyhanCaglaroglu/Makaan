using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;
using Makaan.WebUI.Models;

namespace Makaan.WebUI.Areas.Admin.Models
{
    public class SenderedMessagesAndUsersViewModel
    {
        public List<ResultSenderedMessageDto> resultSenderedMessageDtos { get; set; }
        public List<UserDetailViewModel> userDetailViewModels { get; set; }
    }
}
