using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Comment.Entities
{
    public class MemberComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MemberCommentId { get; set; }
        public string Comment { get; set; }
        public string MemberName { get; set; }
        public bool Status { get; set; }
    }
}
