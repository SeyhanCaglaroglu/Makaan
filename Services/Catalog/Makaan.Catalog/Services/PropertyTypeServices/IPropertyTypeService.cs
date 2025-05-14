using Makaan.Catalog.Dtos.PropertyTypeDtos;

namespace Makaan.Catalog.Services.PropertyTypeServices
{
    public interface IPropertyTypeService
    {
        Task<List<ResultPropertyTypeDto>> GetAllResultPropertyTypeAsync();
        Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto);
        Task UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto);
        Task DeletePropertyTypeAsync(string id);
        Task<GetByIdPropertyTypeDto> GetByIdPropertyTypeAsync(string id);
    }
}
