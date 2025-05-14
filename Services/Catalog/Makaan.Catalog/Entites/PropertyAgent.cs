using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class PropertyAgent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyAgentId { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string? FacebookAdress { get; set; }
        public string? TwitterAdress { get; set; }
        public string? InstagramAdress { get; set; }
    }
}
