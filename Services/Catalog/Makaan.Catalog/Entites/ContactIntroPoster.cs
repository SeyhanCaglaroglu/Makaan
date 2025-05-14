using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Makaan.Catalog.Entites
{
    public class ContactIntroPoster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactIntroPosterId { get; set; }
        public string PosterUrl { get; set; }
    }
}
