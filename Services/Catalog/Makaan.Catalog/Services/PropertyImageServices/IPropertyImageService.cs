using Makaan.Catalog.Dtos.PropertyImageDtos;

namespace Makaan.Catalog.Services.PropertyImageServices
{
    public interface IPropertyImageService
    {
        Task<List<ResultPropertyImageDto>> GetAllResultPropertyImageAsync();
        Task CreatePropertyImageAsync(CreatePropertyImageDto createPropertyImageDto);
        Task UpdatePropertyImageAsync(UpdatePropertyImageDto updatePropertyImageDto);
        Task DeletePropertyImageAsync(string id);
        Task<GetByIdPropertyImageDto> GetByIdPropertyImageAsync(string id);
        Task<List<ResultPropertyImageDto>> GetByPropertyIdPropertyImageAsync(string id);
    }
}
