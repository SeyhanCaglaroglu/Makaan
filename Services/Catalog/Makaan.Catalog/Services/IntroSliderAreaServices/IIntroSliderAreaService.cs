using Makaan.Catalog.Dtos.IntroSliderAreaDtos;

namespace Makaan.Catalog.Services.IntroSliderAreaServices
{
    public interface IIntroSliderAreaService
    {
        Task<List<ResultIntroSliderAreaDto>> GetAllResultIntroSliderAreaAsync();
        Task CreateIntroSliderAreaAsync(CreateIntroSliderAreaDto createIntroSliderAreaDto);
        Task UpdateIntroSliderAreaAsync(UpdateIntroSliderAreaDto updateIntroSliderAreaDto);
        Task DeleteIntroSliderAreaAsync(string id);
        Task<GetByIdIntroSliderAreaDto> GetByIdIntroSliderAreaAsync(string id);
    }
}
