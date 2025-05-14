using Makaan.DtoLayer.CatalogDtos.PropertyIntroPosterDtos;

namespace Makaan.WebUI.Services.PropertyIntroPosterServices
{
    public interface IPropertyIntroPosterService
    {
        Task<List<ResultPropertyIntroPosterDto>> GetAllResultPropertyIntroPosterAsync();
        Task CreatePropertyIntroPosterAsync(CreatePropertyIntroPosterDto createPropertyIntroPosterDto);
        Task UpdatePropertyIntroPosterAsync(GetByIdPropertyIntroPosterDto getByIdPropertyIntroPosterDto);
        Task DeletePropertyIntroPosterAsync(string id);
        Task<GetByIdPropertyIntroPosterDto> GetByIdPropertyIntroPosterAsync(string id);
    }
}
