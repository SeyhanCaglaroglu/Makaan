using Makaan.DtoLayer.CommentDtos.MemberCommentDtos;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Makaan.WebUI.Services.MemberCommentServices
{
    public class MemberCommentService : IMemberCommentService
    {
        private readonly HttpClient _httpClient;
        public MemberCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateMemberCommentAsync(CreateMemberCommentDto createMemberCommentDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateMemberCommentDto>("MemberComments/CreateMemberComment", createMemberCommentDto);
        }

        public async Task DeleteMemberCommentAsync(string id)
        {
            await _httpClient.DeleteAsync("MemberComments/DeleteMemberComment?id=" + id);
        }

        public async Task<List<ResultMemberCommentDto>> GetAllActiveMemberCommentAsync()
        {
            var responseMessage = await _httpClient.GetAsync("MemberComments/GetAllActiveMemberComment");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultMemberCommentDto>>();

            return values ?? new List<ResultMemberCommentDto>();
        }

        public async Task<List<ResultMemberCommentDto>> GetAllResultMemberCommentAsync()
        {
            var responseMessage = await _httpClient.GetAsync("MemberComments/GetAllMemberComment");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultMemberCommentDto>>();

            return values ?? new List<ResultMemberCommentDto>();
        }

        public async Task<GetByIdMemberCommentDto> GetByIdMemberCommentAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("MemberComments/GetByIdMemberComment?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"API isteği başarısız oldu. Kod: {(int)responseMessage.StatusCode}, Mesaj: {errorContent}");
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdMemberCommentDto>();

            return value ?? new GetByIdMemberCommentDto();
        }

        public async Task UpdateMemberCommentAsync(GetByIdMemberCommentDto getByIdMemberCommentDto)
        {
            var result = await _httpClient.PutAsJsonAsync<GetByIdMemberCommentDto>("MemberComments/UpdateMemberComment", getByIdMemberCommentDto);
        }


    }
}
