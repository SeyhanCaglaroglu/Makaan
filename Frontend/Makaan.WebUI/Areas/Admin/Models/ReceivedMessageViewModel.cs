using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;
using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;

namespace Makaan.WebUI.Areas.Admin.Models
{
    public class ReceivedMessageViewModel
    {
        public GetByIdReceivedMessageDto getByIdReceivedMessageDto { get; set; }
        public CreateSenderedMessageDto createSenderedMessageDto { get; set; }
    }
}
