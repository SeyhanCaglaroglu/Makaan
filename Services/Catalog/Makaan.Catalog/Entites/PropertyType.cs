using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class PropertyType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyTypeId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyIconUrl { get; set; }
    }
}
