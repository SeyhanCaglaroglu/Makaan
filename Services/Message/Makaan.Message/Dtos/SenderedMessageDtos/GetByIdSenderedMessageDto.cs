namespace Makaan.Message.Dtos.SenderedMessageDtos
{
    public class GetByIdSenderedMessageDto
    {
        public string SenderedMessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReceiveId { get; set; }
    }
}
