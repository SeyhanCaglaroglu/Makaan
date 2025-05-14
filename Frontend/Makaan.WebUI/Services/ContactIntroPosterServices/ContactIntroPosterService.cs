using Makaan.DtoLayer.CatalogDtos.ContactIntroPosterDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.ContactIntroPosterServices
{
    public class ContactIntroPosterService : IContactIntroPosterService
    {
        private readonly HttpClient _httpClient;
        public ContactIntroPosterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactIntroPosterAsync(CreateContactIntroPosterDto createContactIntroPosterDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateContactIntroPosterDto>("ContactIntroPosters/CreateContactIntroPoster", createContactIntroPosterDto);
        }

        public async Task DeleteContactIntroPosterAsync(string id)
        {
            await _httpClient.DeleteAsync("ContactIntroPosters/DeleteContactIntroPoster?id=" + id);
        }

        public async Task<List<ResultContactIntroPosterDto>> GetAllResultContactIntroPosterAsync()
        {
            var responseMessage = await _httpClient.GetAsync("ContactIntroPosters/GetAllContactIntroPoster");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactIntroPosterDto>>();

            return values ?? new List<ResultContactIntroPosterDto>();
        }

        public async Task<GetByIdContactIntroPosterDto> GetByIdContactIntroPosterAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("ContactIntroPosters/GetByIdContactIntroPoster?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdContactIntroPosterDto>();

            return value ?? new GetByIdContactIntroPosterDto();
        }

        public async Task UpdateContactIntroPosterAsync(GetByIdContactIntroPosterDto getByIdContactIntroPosterDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdContactIntroPosterDto>("ContactIntroPosters/UpdateContactIntroPoster", getByIdContactIntroPosterDto);
        }


    }
}
