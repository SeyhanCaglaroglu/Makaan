using Makaan.DtoLayer.CatalogDtos.ContactDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;
        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateContactDto>("Contacts/CreateContact", createContactDto);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _httpClient.DeleteAsync("Contacts/DeleteContact?id=" + id);
        }

        public async Task<List<ResultContactDto>> GetAllResultContactAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Contacts/GetAllContact");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>();

            return values ?? new List<ResultContactDto>();
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Contacts/GetByIdContact?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdContactDto>();

            return value ?? new GetByIdContactDto();
        }

        public async Task UpdateContactAsync(GetByIdContactDto getByIdContactDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdContactDto>("Contacts/UpdateContact", getByIdContactDto);
        }


    }
}
