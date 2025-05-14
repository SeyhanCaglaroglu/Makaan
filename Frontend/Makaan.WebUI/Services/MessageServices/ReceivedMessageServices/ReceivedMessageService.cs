using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;

namespace Makaan.WebUI.Services.MessageServices.ReceivedMessageServices
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly HttpClient _httpClient;
        public ReceivedMessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateReceivedMessageAsync(CreateReceivedMessageDto createReceivedMessageDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateReceivedMessageDto>("ReceivedMessages/CreateReceivedMessage", createReceivedMessageDto);
        }

        public async Task DeleteAllReceivedMessageAsync()
        {
            await _httpClient.DeleteAsync("ReceivedMessages/DeleteAllReceivedMessage");
        }

        public async Task DeleteReceivedMessageAsync(string id)
        {
            await _httpClient.DeleteAsync("ReceivedMessages/DeleteReceivedMessage?id=" + id);
        }

        public async Task<List<ResultReceivedMessageDto>> GetAllReceivedMessageBySenderIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("ReceivedMessages/GetAllReceivedMessageBySenderId?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultReceivedMessageDto>>();

            return values ?? new List<ResultReceivedMessageDto>();
        }
        

        public async Task<List<ResultReceivedMessageDto>> GetAllResultReceivedMessageAsync()
        {
            var responseMessage = await _httpClient.GetAsync("ReceivedMessages/GetAllReceivedMessage");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultReceivedMessageDto>>();

            return values ?? new List<ResultReceivedMessageDto>();
        }

        public async Task<GetByIdReceivedMessageDto> GetByIdReceivedMessageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("ReceivedMessages/GetByIdReceivedMessage?id="+id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdReceivedMessageDto>();

            return value ?? new GetByIdReceivedMessageDto();
        }
    }
}
