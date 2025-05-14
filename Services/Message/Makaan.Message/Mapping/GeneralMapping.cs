using AutoMapper;
using Makaan.Message.Dtos.ReceivedMessageDtos;
using Makaan.Message.Dtos.SenderedMessageDtos;
using Makaan.Message.Entities;

namespace Makaan.Message.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //ReceivedMessage
            CreateMap<ReceivedMessage, ResultReceivedMessageDto>().ReverseMap();
            CreateMap<ReceivedMessage, CreateReceivedMessageDto>().ReverseMap();
            CreateMap<ReceivedMessage, GetByIdReceivedMessageDto>().ReverseMap();

            //SenderedMessage
            CreateMap<SenderedMessage, ResultSenderedMessageDto>().ReverseMap();
            CreateMap<SenderedMessage, CreateSenderedMessageDto>().ReverseMap();
            CreateMap<SenderedMessage, GetByIdSenderedMessageDto>().ReverseMap();
        }
    }
}
