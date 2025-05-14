using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public int Sqft { get; set; }
        public int BedCount { get; set; }
        public int BathCount { get; set; }    
        public string PropertyTypeId { get; set; }
        [BsonIgnore]
        public PropertyType PropertyType { get; set; }

        public string PropertyAgentId { get; set; }
        [BsonIgnore]
        public PropertyAgent PropertyAgent { get; set; }
    }
}
