using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class PropertyDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyDetailId { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string PropertyId { get; set; }

        [BsonIgnore]
        public Property Property { get; set; }
    }
}
