using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Message.Entities
{
    public class ReceivedMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReceivedMessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
    }
}
