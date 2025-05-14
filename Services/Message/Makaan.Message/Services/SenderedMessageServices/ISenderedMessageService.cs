using Makaan.Message.Dtos.SenderedMessageDtos;

namespace Makaan.Message.Services.SenderedMessageServices
{
    public interface ISenderedMessageService
    {
        Task<List<ResultSenderedMessageDto>> GetAllResultSenderedMessageAsync();
        Task CreateSenderedMessageAsync(CreateSenderedMessageDto createSenderedMessageDto);
        Task<GetByIdSenderedMessageDto> GetByIdSenderedMessageAsync(string id);
        Task<List<ResultSenderedMessageDto>> GetAllSenderedMessageByReceiveIdAsync(string id);
        Task DeleteSenderedMessageAsync(string id);
        Task DeleteAllSenderedMessageAsync();
    }
}
