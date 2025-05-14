using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;

namespace Makaan.WebUI.Services.PropertyTypeServices
{
    public interface IPropertyTypeService
    {
        Task<List<ResultPropertyTypeDto>> GetAllPropertyTypeAsync();
        Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto);
        Task UpdatePropertyTypeAsync(GetByIdPropertyTypeDto getByIdPropertyTypeDto);
        Task DeletePropertyTypeAsync(string id);
        Task<GetByIdPropertyTypeDto> GetByIdPropertyTypeAsync(string id);
    }
}
