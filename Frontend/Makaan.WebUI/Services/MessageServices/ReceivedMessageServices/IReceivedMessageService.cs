using Makaan.DtoLayer.MessageDtos.ReceivedMessageDtos;

namespace Makaan.WebUI.Services.MessageServices.ReceivedMessageServices
{
    public interface IReceivedMessageService
    {
        Task<List<ResultReceivedMessageDto>> GetAllResultReceivedMessageAsync();
        Task CreateReceivedMessageAsync(CreateReceivedMessageDto createReceivedMessageDto);
        Task<GetByIdReceivedMessageDto> GetByIdReceivedMessageAsync(string id);
        Task<List<ResultReceivedMessageDto>> GetAllReceivedMessageBySenderIdAsync(string id);
        Task DeleteReceivedMessageAsync(string id);
        Task DeleteAllReceivedMessageAsync();
    }
}
