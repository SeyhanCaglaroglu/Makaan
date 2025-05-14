using Makaan.DtoLayer.CatalogDtos.ContactDtos;

namespace Makaan.WebUI.Services.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllResultContactAsync();
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task UpdateContactAsync(GetByIdContactDto getByIdContactDto);
        Task DeleteContactAsync(string id);
        Task<GetByIdContactDto> GetByIdContactAsync(string id);
    }
}
