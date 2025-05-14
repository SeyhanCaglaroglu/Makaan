using Makaan.DtoLayer.CatalogDtos.PropertyIntroPosterDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.PropertyIntroPosterServices
{
    public class PropertyIntroPosterService : IPropertyIntroPosterService
    {
        private readonly HttpClient _httpClient;
        public PropertyIntroPosterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyIntroPosterAsync(CreatePropertyIntroPosterDto createPropertyIntroPosterDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreatePropertyIntroPosterDto>("PropertyIntroPosters/CreatePropertyIntroPoster", createPropertyIntroPosterDto);
        }

        public async Task DeletePropertyIntroPosterAsync(string id)
        {
            await _httpClient.DeleteAsync("PropertyIntroPosters/DeletePropertyIntroPoster?id=" + id);
        }

        public async Task<List<ResultPropertyIntroPosterDto>> GetAllResultPropertyIntroPosterAsync()
        {
            var responseMessage = await _httpClient.GetAsync("PropertyIntroPosters/GetAllPropertyIntroPoster");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyIntroPosterDto>>();

            return values ?? new List<ResultPropertyIntroPosterDto>();
        }

        public async Task<GetByIdPropertyIntroPosterDto> GetByIdPropertyIntroPosterAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyIntroPosters/GetByIdPropertyIntroPoster?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyIntroPosterDto>();

            return value ?? new GetByIdPropertyIntroPosterDto();
        }

        public async Task UpdatePropertyIntroPosterAsync(GetByIdPropertyIntroPosterDto getByIdPropertyIntroPosterDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdPropertyIntroPosterDto>("PropertyIntroPosters/UpdatePropertyIntroPoster", getByIdPropertyIntroPosterDto);
        }


    }
}
