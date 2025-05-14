using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Notification.Entities
{
    public class AccountNotification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AccountNotificationId { get; set; }
        public string Content { get; set; }
    }
}
