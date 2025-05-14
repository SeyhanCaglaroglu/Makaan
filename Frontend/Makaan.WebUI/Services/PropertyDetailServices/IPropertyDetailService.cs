using Makaan.DtoLayer.CatalogDtos.PropertyDetailDtos;

namespace Makaan.WebUI.Services.PropertyDetailServices
{
    public interface IPropertyDetailService
    {
        Task<List<ResultPropertyDetailDto>> GetAllResultPropertyDetailAsync();
        Task CreatePropertyDetailAsync(CreatePropertyDetailDto createPropertyDetailDto);
        Task UpdatePropertyDetailAsync(GetByIdPropertyDetailDto getByIdPropertyDetailDto);
        Task DeletePropertyDetailAsync(string id);
        Task<GetByIdPropertyDetailDto> GetByIdPropertyDetailAsync(string id);
        Task<List<ResultPropertyDetailDto>> GetByPropertyIdPropertyDetailAsync(string id);
    }
}
