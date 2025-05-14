namespace Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos
{
    public class GetByIdReceivedMessageDto
    {
        public string ReceivedMessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
    }
}
