using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IntroPosterUrl { get; set; }
    }
}
