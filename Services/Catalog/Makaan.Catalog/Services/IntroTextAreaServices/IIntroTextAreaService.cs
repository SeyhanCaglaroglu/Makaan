using Makaan.Catalog.Dtos.IntroTextAreaDtos;

namespace Makaan.Catalog.Services.IntroTextAreaServices
{
    public interface IIntroTextAreaService
    {
        Task<List<ResultIntroTextAreaDto>> GetAllIntroTextAreaAsync();
        Task CreateIntroTextAreaAsync(CreateIntroTextAreaDto createIntroTextAreaDto);
        Task UpdateIntroTextAreaAsync(UpdateIntroTextAreaDto updateIntroTextAreaDto);
        Task DeleteIntroTextAreaAsync(string id);
        Task<GetByIdIntroTextAreaDto> GetByIdIntroTextAreaAsync(string id);
    }
}
