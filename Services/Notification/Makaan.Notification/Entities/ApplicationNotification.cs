using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Makaan.Notification.Entities
{
    public class ApplicationNotification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ApplicationNotificationId { get; set; }
        public string Content { get; set; }
    }
}
