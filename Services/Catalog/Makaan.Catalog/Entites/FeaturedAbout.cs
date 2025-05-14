using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class FeaturedAbout
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeaturedAboutId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IntroPosterUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Tag1 { get; set; } 
        public string Tag2 { get; set; } 
        public string Tag3 { get; set; } 
    }
}
