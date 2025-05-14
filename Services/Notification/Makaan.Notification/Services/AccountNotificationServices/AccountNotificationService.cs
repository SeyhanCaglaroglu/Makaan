using AutoMapper;
using Makaan.Notification.Dtos.AccountNotificationDtos;
using Makaan.Notification.Entities;
using Makaan.Notification.Settings;
using MongoDB.Driver;

namespace Makaan.Notification.Services.AccountNotificationServices
{
    public class AccountNotificationService : IAccountNotificationService
    {
        private readonly IMongoCollection<AccountNotification> _accountNotificationCollection;
        private readonly IMapper _mapper;

        public AccountNotificationService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _accountNotificationCollection = database.GetCollection<AccountNotification>(_databaseSettings.AccountNotificationCollectionName);
            _mapper = mapper;
        }

        public async Task CreateAccountNotificationAsync(CreateAccountNotificationDto createAccountNotificationDto)
        {
            var value = _mapper.Map<AccountNotification>(createAccountNotificationDto);
            await _accountNotificationCollection.InsertOneAsync(value);
        }

        public async Task DeleteAllAccountNotificationAsync()
        {
            await _accountNotificationCollection.DeleteManyAsync(FilterDefinition<AccountNotification>.Empty);
        }

        public async Task DeleteAccountNotificationAsync(string id)
        {
            await _accountNotificationCollection.DeleteOneAsync(x => x.AccountNotificationId == id);
        }

        public async Task<List<ResultAccountNotificationDto>> GetAllResultAccountNotificationAsync()
        {
            var value = await _accountNotificationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultAccountNotificationDto>>(value);
        }


    }
}
