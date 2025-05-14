using Makaan.DtoLayer.CatalogDtos.ContactIntroPosterDtos;

namespace Makaan.WebUI.Services.ContactIntroPosterServices
{
    public interface IContactIntroPosterService
    {
        Task<List<ResultContactIntroPosterDto>> GetAllResultContactIntroPosterAsync();
        Task CreateContactIntroPosterAsync(CreateContactIntroPosterDto createContactIntroPosterDto);
        Task UpdateContactIntroPosterAsync(GetByIdContactIntroPosterDto getByIdContactIntroPosterDto);
        Task DeleteContactIntroPosterAsync(string id);
        Task<GetByIdContactIntroPosterDto> GetByIdContactIntroPosterAsync(string id);
    }
}
