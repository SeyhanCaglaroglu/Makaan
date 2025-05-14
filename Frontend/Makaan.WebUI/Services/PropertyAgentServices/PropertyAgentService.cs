using Makaan.DtoLayer.CatalogDtos.PropertyAgentDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.PropertyAgentServices
{
    public class PropertyAgentService : IPropertyAgentService
    {
        private readonly HttpClient _httpClient;
        public PropertyAgentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyAgentAsync(CreatePropertyAgentDto createPropertyAgentDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreatePropertyAgentDto>("PropertyAgents/CreatePropertyAgent", createPropertyAgentDto);
        }

        public async Task DeletePropertyAgentAsync(string id)
        {
            await _httpClient.DeleteAsync("PropertyAgents/DeletePropertyAgent?id=" + id);
        }

        public async Task<List<ResultPropertyAgentDto>> GetAllResultPropertyAgentAsync()
        {
            var responseMessage = await _httpClient.GetAsync("PropertyAgents/GetAllPropertyAgent");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyAgentDto>>();

            return values ?? new List<ResultPropertyAgentDto>();
        }

        public async Task<GetByIdPropertyAgentDto> GetByIdPropertyAgentAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyAgents/GetByIdPropertyAgent?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyAgentDto>();

            return value ?? new GetByIdPropertyAgentDto();
        }

        public async Task<GetByIdPropertyAgentDto> GetPropertyAgentByTitleAsync(string title)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyAgents/GetPropertyAgentByTitle?title=" + title);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyAgentDto>();

            return value ?? new GetByIdPropertyAgentDto();
        }

        public async Task UpdatePropertyAgentAsync(GetByIdPropertyAgentDto getByIdPropertyAgentDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdPropertyAgentDto>("PropertyAgents/UpdatePropertyAgent", getByIdPropertyAgentDto);
        }


    }
}
