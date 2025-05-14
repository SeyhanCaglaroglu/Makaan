using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Notification.Entities
{
    public class CommentNotification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CommentNotificationId { get; set; }
        public string Content { get; set; }
    }
}
