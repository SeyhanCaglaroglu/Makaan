using Makaan.DtoLayer.CatalogDtos.PropertyDtos;

namespace Makaan.WebUI.Services.PropertyServices
{
    public interface IPropertyService
    {
        Task<List<ResultPropertyDto>> GetAllResultPropertyAsync();
        Task CreatePropertyAsync(CreatePropertyDto createPropertyDto);
        Task UpdatePropertyAsync(GetByIdPropertyDto getByIdPropertyDto);
        Task DeletePropertyAsync(string id);
        Task<GetByIdPropertyDto> GetByIdPropertyAsync(string id);
        Task<List<ResultPropertyDto>> GetPropertiesByPropertyTypeId(string id);
        Task<int> GetPropertyCountByPropertyTypeId(string id);
        Task<List<ResultPropertyDto>> GetPropertiesByPropertyAgentId(string id);
    }
}
