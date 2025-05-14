using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyImageId { get; set; }
        public string ImageUrl { get; set; }
        public string PropertyId { get; set; }

        [BsonIgnore]
        public Property Property { get; set; }
    }
}
