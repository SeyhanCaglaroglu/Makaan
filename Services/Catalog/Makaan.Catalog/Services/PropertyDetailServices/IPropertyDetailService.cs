using Makaan.Catalog.Dtos.PropertyDetailDtos;

namespace Makaan.Catalog.Services.PropertyDetailServices
{
    public interface IPropertyDetailService
    {
        Task<List<ResultPropertyDetailDto>> GetAllResultPropertyDetailAsync();
        Task CreatePropertyDetailAsync(CreatePropertyDetailDto createPropertyDetailDto);
        Task UpdatePropertyDetailAsync(UpdatePropertyDetailDto updatePropertyDetailDto);
        Task DeletePropertyDetailAsync(string id);
        Task<GetByIdPropertyDetailDto> GetByIdPropertyDetailAsync(string id);
        Task<List<ResultPropertyDetailDto>> GetByPropertyIdPropertyDetailAsync(string id);
    }
}
