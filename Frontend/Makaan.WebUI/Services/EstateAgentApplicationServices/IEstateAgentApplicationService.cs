using Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos;

namespace Makaan.WebUI.Services.EstateAgentApplicationServices
{
    public interface IEstateAgentApplicationService
    {
        Task<List<ResultEstateAgentApplicationDto>> GetAllResultEstateAgentApplicationAsync();
        Task CreateEstateAgentApplicationAsync(CreateEstateAgentApplicationDto createEstateAgentApplicationDto);
        Task DeleteEstateAgentApplicationAsync(string id);
        Task<GetByIdEstateAgentApplicationDto> GetByIdEstateAgentApplicationAsync(string id);
        Task<GetByIdEstateAgentApplicationDto> GetEstateAgentApplicationByAuthNameAsync(string authName);
    }
}
