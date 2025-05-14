using Makaan.DtoLayer.CatalogDtos.PropertyDetailDtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.PropertyDetailServices
{
    public class PropertyDetailService : IPropertyDetailService
    {
        private readonly HttpClient _httpClient;
        public PropertyDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyDetailAsync(CreatePropertyDetailDto createPropertyDetailDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreatePropertyDetailDto>("PropertyDetails/CreatePropertyDetail", createPropertyDetailDto);
        }

        public async Task DeletePropertyDetailAsync(string id)
        {
            await _httpClient.DeleteAsync("PropertyDetails/DeletePropertyDetail?id=" + id);
        }

        public async Task<List<ResultPropertyDetailDto>> GetAllResultPropertyDetailAsync()
        {
            var responseMessage = await _httpClient.GetAsync("PropertyDetails/GetAllPropertyDetail");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyDetailDto>>();

            return values ?? new List<ResultPropertyDetailDto>();
        }

        public async Task<GetByIdPropertyDetailDto> GetByIdPropertyDetailAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyDetails/GetByIdPropertyDetail?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdPropertyDetailDto>();

            return value ?? new GetByIdPropertyDetailDto();
        }

        public async Task<List<ResultPropertyDetailDto>> GetByPropertyIdPropertyDetailAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("PropertyDetails/GetByPropertyIdPropertyDetail?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultPropertyDetailDto>>();

            return values ?? new  List<ResultPropertyDetailDto>();
        }

        public async Task UpdatePropertyDetailAsync(GetByIdPropertyDetailDto getByIdPropertyDetailDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdPropertyDetailDto>("PropertyDetails/UpdatePropertyDetail", getByIdPropertyDetailDto);
        }


    }
}
