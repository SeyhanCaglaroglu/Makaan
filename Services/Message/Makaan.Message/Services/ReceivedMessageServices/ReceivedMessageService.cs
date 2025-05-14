using AutoMapper;
using Makaan.Message.Dtos.ReceivedMessageDtos;
using Makaan.Message.Entities;
using Makaan.Message.Settings;
using MongoDB.Driver;

namespace Makaan.Message.Services.ReceivedMessageServices
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly IMongoCollection<ReceivedMessage> _receivedMessageCollection;
        private readonly IMapper _mapper;

        public ReceivedMessageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _receivedMessageCollection = database.GetCollection<ReceivedMessage>(_databaseSettings.ReceivedMessageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateReceivedMessageAsync(CreateReceivedMessageDto createReceivedMessageDto)
        {
            var value = _mapper.Map<ReceivedMessage>(createReceivedMessageDto);
            await _receivedMessageCollection.InsertOneAsync(value);
        }

        public async Task DeleteAllReceivedMessageAsync()
        {
            await _receivedMessageCollection.DeleteManyAsync(FilterDefinition<ReceivedMessage>.Empty);
        }

        public async Task DeleteReceivedMessageAsync(string id)
        {
            await _receivedMessageCollection.DeleteOneAsync(x => x.ReceivedMessageId == id);
        }

        public async Task<List<ResultReceivedMessageDto>> GetAllReceivedMessageBySenderIdAsync(string id)
        {
            var values = await _receivedMessageCollection.Find<ReceivedMessage>(x=>x.SenderId == id).ToListAsync();
            return _mapper.Map<List<ResultReceivedMessageDto>>(values);
        }

        public async Task<List<ResultReceivedMessageDto>> GetAllResultReceivedMessageAsync()
        {
            var value = await _receivedMessageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultReceivedMessageDto>>(value);
        }

        public async Task<GetByIdReceivedMessageDto> GetByIdReceivedMessageAsync(string id)
        {
            var value = await _receivedMessageCollection.Find<ReceivedMessage>(x => x.ReceivedMessageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdReceivedMessageDto>(value);
        }
    }
}
