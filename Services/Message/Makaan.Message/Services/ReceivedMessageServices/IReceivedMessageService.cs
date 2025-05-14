using Makaan.Message.Dtos.ReceivedMessageDtos;

namespace Makaan.Message.Services.ReceivedMessageServices
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
