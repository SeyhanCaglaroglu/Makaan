using Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.EstateAgentApplicationServices
{
    public class EstateAgentApplicationService : IEstateAgentApplicationService
    {
        private readonly HttpClient _httpClient;
        public EstateAgentApplicationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEstateAgentApplicationAsync(CreateEstateAgentApplicationDto createEstateAgentApplicationDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateEstateAgentApplicationDto>("EstateAgentApplications/CreateEstateAgentApplication", createEstateAgentApplicationDto);
        }

        public async Task DeleteEstateAgentApplicationAsync(string id)
        {
            await _httpClient.DeleteAsync("EstateAgentApplications/DeleteEstateAgentApplication?id=" + id);
        }

        public async Task<List<ResultEstateAgentApplicationDto>> GetAllResultEstateAgentApplicationAsync()
        {
            var responseMessage = await _httpClient.GetAsync("EstateAgentApplications/GetAllEstateAgentApplication");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultEstateAgentApplicationDto>>();

            return values ?? new List<ResultEstateAgentApplicationDto>();
        }

        public async Task<GetByIdEstateAgentApplicationDto> GetByIdEstateAgentApplicationAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("EstateAgentApplications/GetByIdEstateAgentApplication?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdEstateAgentApplicationDto>();

            return value ?? new GetByIdEstateAgentApplicationDto();
        }

        public async Task<GetByIdEstateAgentApplicationDto> GetEstateAgentApplicationByAuthNameAsync(string authName)
        {
            var responseMessage = await _httpClient.GetAsync("EstateAgentApplications/GetEstateAgentApplicationByAuthName?authName=" + authName);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdEstateAgentApplicationDto>();

            return value ?? new GetByIdEstateAgentApplicationDto();
        }
    }
}
