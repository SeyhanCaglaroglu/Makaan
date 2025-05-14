using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.PropertyTypeServices
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly HttpClient _httpClient;
        public PropertyTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreatePropertyTypeDto>("PropertyTypes/CreatePropertyType", createPropertyTypeDto);
        }

        public async Task DeletePropertyTypeAsync(string id)
        {
            await _httpClient.DeleteAsync("PropertyTypes/DeletePropertyType?id=" + id);
        }

        public async Task<List<ResultPropertyTypeDto>> GetAllPropertyTypeAsync()
        {
            var responseMessage = await _httpClient.GetAsync("PropertyTypes/GetAllPropertyType");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyTypeDto>>();

            return values ?? new List<ResultPropertyTypeDto>();
        }

        

        public async Task<GetByIdPropertyTypeDto> GetByIdPropertyTypeAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyTypes/GetByIdPropertyType?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyTypeDto>();

            return value ?? new GetByIdPropertyTypeDto();
        }

        public async Task UpdatePropertyTypeAsync(GetByIdPropertyTypeDto getByIdPropertyTypeDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdPropertyTypeDto>("PropertyTypes/UpdatePropertyType", getByIdPropertyTypeDto);
        }
    }
}
