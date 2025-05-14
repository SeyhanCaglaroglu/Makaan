using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class PropertyIntroPoster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyIntroPosterId { get; set; }
        public string PosterUrl { get; set; }
    }
}
