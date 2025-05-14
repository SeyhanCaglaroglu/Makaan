using AutoMapper;
using Makaan.Message.Dtos.SenderedMessageDtos;
using Makaan.Message.Entities;
using Makaan.Message.Settings;
using MongoDB.Driver;

namespace Makaan.Message.Services.SenderedMessageServices
{
    public class SenderedMessageService : ISenderedMessageService
    {
        private readonly IMongoCollection<SenderedMessage> _senderedMessageCollection;
        private readonly IMapper _mapper;

        public SenderedMessageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _senderedMessageCollection = database.GetCollection<SenderedMessage>(_databaseSettings.SenderedMessageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateSenderedMessageAsync(CreateSenderedMessageDto createSenderedMessageDto)
        {
            var value = _mapper.Map<SenderedMessage>(createSenderedMessageDto);
            await _senderedMessageCollection.InsertOneAsync(value);
        }

        public async Task DeleteAllSenderedMessageAsync()
        {
            await _senderedMessageCollection.DeleteManyAsync(FilterDefinition<SenderedMessage>.Empty);
        }

        public async Task DeleteSenderedMessageAsync(string id)
        {
            await _senderedMessageCollection.DeleteOneAsync(x => x.SenderedMessageId == id);
        }

        public async Task<List<ResultSenderedMessageDto>> GetAllSenderedMessageByReceiveIdAsync(string id)
        {
            var values = await _senderedMessageCollection.Find<SenderedMessage>(x => x.ReceiveId == id).ToListAsync();
            return _mapper.Map<List<ResultSenderedMessageDto>>(values);
        }

        public async Task<List<ResultSenderedMessageDto>> GetAllResultSenderedMessageAsync()
        {
            var value = await _senderedMessageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultSenderedMessageDto>>(value);
        }

        public async Task<GetByIdSenderedMessageDto> GetByIdSenderedMessageAsync(string id)
        {
            var value = await _senderedMessageCollection.Find<SenderedMessage>(x => x.SenderedMessageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdSenderedMessageDto>(value);
        }
    }
}
