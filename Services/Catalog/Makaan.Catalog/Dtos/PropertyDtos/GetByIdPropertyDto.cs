namespace Makaan.Catalog.Dtos.PropertyDtos
{
    public class GetByIdPropertyDto
    {
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
        public string PropertyAgentId { get; set; }
        public string PropertyTypeId { get; set; }
    }
}
