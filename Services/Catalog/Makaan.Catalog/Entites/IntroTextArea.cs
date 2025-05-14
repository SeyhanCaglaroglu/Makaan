using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class IntroTextArea
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IntroTextAreaId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
