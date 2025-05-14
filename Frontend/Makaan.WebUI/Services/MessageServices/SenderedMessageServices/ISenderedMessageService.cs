using Makaan.DtoLayer.MessageDtos.SenderedMessageDtos;

namespace Makaan.WebUI.Services.MessageServices.SenderedMessageServices
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
