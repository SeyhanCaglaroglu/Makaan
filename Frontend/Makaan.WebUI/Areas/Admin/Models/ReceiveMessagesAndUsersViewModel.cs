using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;
using Makaan.WebUI.Models;

namespace Makaan.WebUI.Areas.Admin.Models
{
    public class ReceiveMessagesAndUsersViewModel
    {
        public List<ResultReceivedMessageDto> resultReceivedMessageDtos { get; set; }
        public List<UserDetailViewModel> userDetailViewModels { get; set; }
    }
}
