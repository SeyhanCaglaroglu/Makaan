using Makaan.Catalog.Dtos.PropertyAgentDtos;

namespace Makaan.Catalog.Services.PropertyAgentServices
{
    public interface IPropertyAgentService
    {
        Task<List<ResultPropertyAgentDto>> GetAllResultPropertyAgentAsync();
        Task CreatePropertyAgentAsync(CreatePropertyAgentDto createPropertyAgentDto);
        Task UpdatePropertyAgentAsync(UpdatePropertyAgentDto updatePropertyAgentDto);
        Task DeletePropertyAgentAsync(string id);
        Task<GetByIdPropertyAgentDto> GetByIdPropertyAgentAsync(string id);
        Task<GetByIdPropertyAgentDto> GetPropertyAgentByTitleAsync(string title);
    }
}
