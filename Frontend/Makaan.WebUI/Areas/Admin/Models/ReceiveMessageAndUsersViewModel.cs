using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;
using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;
using Makaan.WebUI.Models;

namespace Makaan.WebUI.Areas.Admin.Models
{
    public class ReceiveMessageAndUsersViewModel
    {
        public GetByIdReceivedMessageDto getByIdReceivedMessageDto { get; set; }
        public List<UserDetailViewModel> userDetailViewModels { get; set; }
        public CreateSenderedMessageDto createSenderedMessageDto { get; set; }
    }
}
