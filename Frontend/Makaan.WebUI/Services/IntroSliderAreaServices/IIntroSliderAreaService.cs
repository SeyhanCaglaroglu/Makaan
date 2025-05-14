using Makaan.DtoLayer.CatalogDtos.IntroSliderAreaDtos;

namespace Makaan.WebUI.Services.IntroSliderAreaServices
{
    public interface IIntroSliderAreaService
    {
        Task<List<ResultIntroSliderAreaDto>> GetAllIntroSliderAreaAsync();
        Task CreateIntroSliderAreaAsync(CreateIntroSliderAreaDto createIntroSliderAreaDto);
        Task UpdateIntroSliderAreaAsync(GetByIdIntroSliderAreaDto getByIdIntroSliderAreaDto);
        Task DeleteIntroSliderAreaAsync(string id);
        Task<GetByIdIntroSliderAreaDto> GetByIdIntroSliderAreaAsync(string id);
    }
}
