using Makaan.DtoLayer.CatalogDtos.PropertyImageDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.PropertyImageServices
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly HttpClient _httpClient;
        public PropertyImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyImageAsync(CreatePropertyImageDto createPropertyImageDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreatePropertyImageDto>("PropertyImages/CreatePropertyImage", createPropertyImageDto);
        }

        public async Task DeletePropertyImageAsync(string id)
        {
            await _httpClient.DeleteAsync("PropertyImages/DeletePropertyImage?id=" + id);
        }

        public async Task<List<ResultPropertyImageDto>> GetAllResultPropertyImageAsync()
        {
            var responseMessage = await _httpClient.GetAsync("PropertyImages/GetAllPropertyImage");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyImageDto>>();

            return values ?? new List<ResultPropertyImageDto>();
        }

        public async Task<GetByIdPropertyImageDto> GetByIdPropertyImageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyImages/GetByIdPropertyImage?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyImageDto>();

            return value ?? new GetByIdPropertyImageDto();
        }

        public async Task<List<ResultPropertyImageDto>> GetByPropertyIdPropertyImageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyImages/GetByPropertyIdPropertyImage?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyImageDto>>();

            return values ?? new List<ResultPropertyImageDto>();
        }

        public async Task UpdatePropertyImageAsync(GetByIdPropertyImageDto getByIdPropertyImageDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdPropertyImageDto>("PropertyImages/UpdatePropertyImage", getByIdPropertyImageDto);
        }

        
    }
}
