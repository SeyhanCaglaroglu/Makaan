using Makaan.DtoLayer.CatalogDtos.PropertyDetailDtos;
using Makaan.DtoLayer.CatalogDtos.PropertyDtos;
using Makaan.DtoLayer.CatalogDtos.PropertyImageDtos;

namespace Makaan.WebUI.Models
{
    public class PropertyViewModel
    {
        public GetByIdPropertyDto getByIdPropertyDto { get; set; }
        public List<ResultPropertyDetailDto> resultPropertyDetailDto { get; set; }
        public List<ResultPropertyImageDto> resultPropertyImageDto { get; set; }
    }
}
