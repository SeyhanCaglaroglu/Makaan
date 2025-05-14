using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;

namespace Makaan.WebUI.Services.MessageServices.SenderedMessageServices
{
    public class SenderedMessageService : ISenderedMessageService
    {
        private readonly HttpClient _httpClient;
        public SenderedMessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateSenderedMessageAsync(CreateSenderedMessageDto createSenderedMessageDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateSenderedMessageDto>("SenderedMessages/CreateSenderedMessage", createSenderedMessageDto);
        }

        public async Task DeleteAllSenderedMessageAsync()
        {
            await _httpClient.DeleteAsync("SenderedMessages/DeleteAllSenderedMessage");
        }

        public async Task DeleteSenderedMessageAsync(string id)
        {
            await _httpClient.DeleteAsync("SenderedMessages/DeleteSenderedMessage?id=" + id);
        }

        public async Task<List<ResultSenderedMessageDto>> GetAllSenderedMessageByReceiveIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("SenderedMessages/GetAllSenderedMessageByReceiveId?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSenderedMessageDto>>();

            return values ?? new List<ResultSenderedMessageDto>();
        }


        public async Task<List<ResultSenderedMessageDto>> GetAllResultSenderedMessageAsync()
        {
            var responseMessage = await _httpClient.GetAsync("SenderedMessages/GetAllSenderedMessage");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSenderedMessageDto>>();

            return values ?? new List<ResultSenderedMessageDto>();
        }

        public async Task<GetByIdSenderedMessageDto> GetByIdSenderedMessageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("SenderedMessages/GetByIdSenderedMessage?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdSenderedMessageDto>();

            return value ?? new GetByIdSenderedMessageDto();
        }
    }
}
