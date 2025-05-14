using Makaan.DtoLayer.CatalogDtos.FeaturedAboutDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.FeaturedAboutServices
{
    public class FeaturedAboutService : IFeaturedAboutService
    {
        private readonly HttpClient _httpClient;
        public FeaturedAboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeaturedAboutAsync(CreateFeaturedAboutDto createFeaturedAboutDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateFeaturedAboutDto>("FeaturedAbouts/CreateFeaturedAbout", createFeaturedAboutDto);
        }

        public async Task DeleteFeaturedAboutAsync(string id)
        {
            await _httpClient.DeleteAsync("FeaturedAbouts/DeleteFeaturedAbout?id=" + id);
        }

        public async Task<List<ResultFeaturedAboutDto>> GetAllResultFeaturedAboutAsync()
        {
            var responseMessage = await _httpClient.GetAsync("FeaturedAbouts/GetAllFeaturedAbout");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeaturedAboutDto>>();

            return values ?? new List<ResultFeaturedAboutDto>();
        }

        public async Task<GetByIdFeaturedAboutDto> GetByIdFeaturedAboutAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("FeaturedAbouts/GetByIdFeaturedAbout?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdFeaturedAboutDto>();

            return value ?? new GetByIdFeaturedAboutDto();
        }

        public async Task UpdateFeaturedAboutAsync(GetByIdFeaturedAboutDto getByIdFeaturedAboutDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdFeaturedAboutDto>("FeaturedAbouts/UpdateFeaturedAbout", getByIdFeaturedAboutDto);
        }

        
    }
}
