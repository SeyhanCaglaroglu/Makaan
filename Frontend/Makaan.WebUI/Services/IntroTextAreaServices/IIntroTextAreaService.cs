using Makaan.DtoLayer.CatalogDtos.IntroTextAreaDtos;

namespace Makaan.WebUI.Services.IntroTextAreaServices
{
    public interface IIntroTextAreaService
    {
        Task<List<ResultIntroTextAreaDto>> GetAllIntroTextAreaAsync();
        Task CreateIntroTextAreaAsync(CreateIntroTextAreaDto createIntroTextAreaDto);
        Task UpdateIntroTextAreaAsync(GetByIdIntroTextAreaDto getByIdIntroTextAreaDto);
        Task DeleteIntroTextAreaAsync(string id);
        Task<GetByIdIntroTextAreaDto> GetByIdIntroTextAreaAsync(string id);
    }
}
