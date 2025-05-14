using Makaan.Catalog.Dtos.PropertyDtos;

namespace Makaan.Catalog.Services.PropertyServices
{
    public interface IPropertyService
    {
        Task<List<ResultPropertyDto>> GetAllResultPropertyAsync();
        Task CreatePropertyAsync(CreatePropertyDto createPropertyDto);
        Task UpdatePropertyAsync(UpdatePropertyDto updatePropertyDto);
        Task DeletePropertyAsync(string id);
        Task<GetByIdPropertyDto> GetByIdPropertyAsync(string id);
        Task<List<ResultPropertyDto>> GetPropertiesByPropertyTypeId(string id);
        Task<int> GetPropertyCountByPropertyTypeId(string id);
        Task<List<ResultPropertyDto>> GetPropertiesByPropertyAgentId(string id);
    }
}
