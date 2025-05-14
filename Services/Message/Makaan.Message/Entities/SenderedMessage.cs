using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Message.Entities
{
    public class SenderedMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderedMessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReceiveId { get; set; }
    }
}
