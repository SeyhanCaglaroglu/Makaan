using Makaan.DtoLayer.CatalogDtos.IntroSliderAreaDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.IntroSliderAreaServices
{
    public class IntroSliderAreaService : IIntroSliderAreaService
    {
        private readonly HttpClient _httpClient;
        public IntroSliderAreaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateIntroSliderAreaAsync(CreateIntroSliderAreaDto createIntroSliderAreaDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateIntroSliderAreaDto>("IntroSliderAreas/CreateIntroSliderArea", createIntroSliderAreaDto);
        }

        public async Task DeleteIntroSliderAreaAsync(string id)
        {
            await _httpClient.DeleteAsync("IntroSliderAreas/DeleteIntroSliderArea?id=" + id);
        }

        public async Task<List<ResultIntroSliderAreaDto>> GetAllIntroSliderAreaAsync()
        {
            var responseMessage = await _httpClient.GetAsync("IntroSliderAreas/GetAllIntroSliderArea");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultIntroSliderAreaDto>>();

            return values ?? new List<ResultIntroSliderAreaDto>();
        }

        public async Task<GetByIdIntroSliderAreaDto> GetByIdIntroSliderAreaAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("IntroSliderAreas/GetByIdIntroSliderArea?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdIntroSliderAreaDto>();

            return value ?? new GetByIdIntroSliderAreaDto();
        }

        public async Task UpdateIntroSliderAreaAsync(GetByIdIntroSliderAreaDto getByIdIntroSliderAreaDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdIntroSliderAreaDto>("IntroSliderAreas/UpdateIntroSliderArea", getByIdIntroSliderAreaDto);
        }
    }
}
