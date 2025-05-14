using Makaan.Catalog.Dtos.ContactIntroPosterDtos;

namespace Makaan.Catalog.Services.ContactIntroPosterServices
{
    public interface IContactIntroPosterService
    {
        Task<List<ResultContactIntroPosterDto>> GetAllResultContactIntroPosterAsync();
        Task CreateContactIntroPosterAsync(CreateContactIntroPosterDto createContactIntroPosterDto);
        Task UpdateContactIntroPosterAsync(UpdateContactIntroPosterDto updateContactIntroPosterDto);
        Task DeleteContactIntroPosterAsync(string id);
        Task<GetByIdContactIntroPosterDto> GetByIdContactIntroPosterAsync(string id);
    }
}
