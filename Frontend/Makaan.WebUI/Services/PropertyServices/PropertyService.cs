using Makaan.DtoLayer.CatalogDtos.PropertyDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.PropertyServices
{
    public class PropertyService : IPropertyService
    {
        private readonly HttpClient _httpClient;
        public PropertyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyAsync(CreatePropertyDto createPropertyDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreatePropertyDto>("Properties/CreateProperty", createPropertyDto);
        }

        public async Task DeletePropertyAsync(string id)
        {
            await _httpClient.DeleteAsync("Properties/DeleteProperty?id=" + id);
        }

        public async Task<List<ResultPropertyDto>> GetAllResultPropertyAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Properties/GetAllProperty");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyDto>>();

            return values ?? new List<ResultPropertyDto>();
        }

        public async Task<GetByIdPropertyDto> GetByIdPropertyAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Properties/GetByIdProperty?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyDto>();

            return value ?? new GetByIdPropertyDto();
        }

        public async Task<List<ResultPropertyDto>> GetPropertiesByPropertyAgentId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Properties/GetPropertiesByPropertyAgentId?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyDto>>();
            return values ?? new List<ResultPropertyDto>();
        }

        public async Task<List<ResultPropertyDto>> GetPropertiesByPropertyTypeId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Properties/GetPropertiesByPropertyTypeId?id=" + id);

            if(!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyDto>>();
            return values ?? new List<ResultPropertyDto>();
        }

        public async Task<int> GetPropertyCountByPropertyTypeId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Properties/GetPropertyCountByPropertyTypeId?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadAsStringAsync();

            return int.Parse(value);
        }

        public async Task UpdatePropertyAsync(GetByIdPropertyDto getByIdPropertyDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdPropertyDto>("Properties/UpdateProperty", getByIdPropertyDto);
        }


    }
}
