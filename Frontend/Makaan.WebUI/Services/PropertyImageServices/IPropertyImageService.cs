using Makaan.DtoLayer.CatalogDtos.PropertyImageDtos;

namespace Makaan.WebUI.Services.PropertyImageServices
{
    public interface IPropertyImageService
    {
        Task<List<ResultPropertyImageDto>> GetAllResultPropertyImageAsync();
        Task CreatePropertyImageAsync(CreatePropertyImageDto createPropertyImageDto);
        Task UpdatePropertyImageAsync(GetByIdPropertyImageDto getByIdPropertyImageDto);
        Task DeletePropertyImageAsync(string id);
        Task<GetByIdPropertyImageDto> GetByIdPropertyImageAsync(string id);
        Task<List<ResultPropertyImageDto>> GetByPropertyIdPropertyImageAsync(string id);
    }
}
