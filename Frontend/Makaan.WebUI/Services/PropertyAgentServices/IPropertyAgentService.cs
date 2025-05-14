using Makaan.DtoLayer.CatalogDtos.PropertyAgentDtos;

namespace Makaan.WebUI.Services.PropertyAgentServices
{
    public interface IPropertyAgentService
    {
        Task<List<ResultPropertyAgentDto>> GetAllResultPropertyAgentAsync();
        Task CreatePropertyAgentAsync(CreatePropertyAgentDto createPropertyAgentDto);
        Task UpdatePropertyAgentAsync(GetByIdPropertyAgentDto getByIdPropertyAgentDto);
        Task DeletePropertyAgentAsync(string id);
        Task<GetByIdPropertyAgentDto> GetByIdPropertyAgentAsync(string id);
        Task<GetByIdPropertyAgentDto> GetPropertyAgentByTitleAsync(string title);
    }
}
