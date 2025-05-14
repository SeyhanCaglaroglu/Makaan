using Makaan.Catalog.Dtos.PropertyIntroPosterDtos;

namespace Makaan.Catalog.Services.PropertyIntroPosterServices
{
    public interface IPropertyIntroPosterService
    {
        Task<List<ResultPropertyIntroPosterDto>> GetAllResultPropertyIntroPosterAsync();
        Task CreatePropertyIntroPosterAsync(CreatePropertyIntroPosterDto createPropertyIntroPosterDto);
        Task UpdatePropertyIntroPosterAsync(UpdatePropertyIntroPosterDto updatePropertyIntroPosterDto);
        Task DeletePropertyIntroPosterAsync(string id);
        Task<GetByIdPropertyIntroPosterDto> GetByIdPropertyIntroPosterAsync(string id);
    }
}
