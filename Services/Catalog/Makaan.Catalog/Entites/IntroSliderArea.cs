using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Makaan.Catalog.Entites
{
    public class IntroSliderArea
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IntroSliderAreaId { get; set; }
        public string ImageUrl { get; set; }
    }
}
