using Makaan.DtoLayer.CatalogDtos.IntroTextAreaDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.IntroTextAreaServices
{
    public class IntroTextAreaService : IIntroTextAreaService
    {
        private readonly HttpClient _httpClient;
        public IntroTextAreaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateIntroTextAreaAsync(CreateIntroTextAreaDto createIntroTextAreaDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateIntroTextAreaDto>("IntroTextAreas/CreateIntroTextArea",createIntroTextAreaDto);
        }

        public async Task DeleteIntroTextAreaAsync(string id)
        {
            await _httpClient.DeleteAsync("IntroTextAreas/DeleteIntroTextArea?id=" + id);
        }

        public async Task<List<ResultIntroTextAreaDto>> GetAllIntroTextAreaAsync()
        {
            var responseMessage = await _httpClient.GetAsync("IntroTextAreas/GetAllIntroTextArea");

            if(!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultIntroTextAreaDto>>();

            return values ?? new List<ResultIntroTextAreaDto>();
        }

        public async Task<GetByIdIntroTextAreaDto> GetByIdIntroTextAreaAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("IntroTextAreas/GetByIdIntroTextArea?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdIntroTextAreaDto>();

            return value ?? new GetByIdIntroTextAreaDto();
        }

        public async Task UpdateIntroTextAreaAsync(GetByIdIntroTextAreaDto getByIdIntroTextAreaDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdIntroTextAreaDto>("IntroTextAreas/UpdateIntroTextArea", getByIdIntroTextAreaDto);
        }
    }
}
