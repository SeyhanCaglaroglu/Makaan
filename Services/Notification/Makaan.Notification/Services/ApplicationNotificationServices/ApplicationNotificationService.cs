using AutoMapper;
using Makaan.Notification.Dtos.ApplicationNotificationDtos;
using Makaan.Notification.Entities;
using Makaan.Notification.Settings;
using MongoDB.Driver;

namespace Makaan.Notification.Services.ApplicationNotificationServices
{
    public class ApplicationNotificationService : IApplicationNotificationService
    {
        private readonly IMongoCollection<ApplicationNotification> _applicationNotificationCollection;
        private readonly IMapper _mapper;

        public ApplicationNotificationService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _applicationNotificationCollection = database.GetCollection<ApplicationNotification>(_databaseSettings.ApplicationNotificationCollectionName);
            _mapper = mapper;
        }

        public async Task CreateApplicationNotificationAsync(CreateApplicationNotificationDto createApplicationNotificationDto)
        {
            var value = _mapper.Map<ApplicationNotification>(createApplicationNotificationDto);
            await _applicationNotificationCollection.InsertOneAsync(value);
        }

        public async Task DeleteAllApplicationNotificationAsync()
        {
            await _applicationNotificationCollection.DeleteManyAsync(FilterDefinition<ApplicationNotification>.Empty);
        }

        public async Task DeleteApplicationNotificationAsync(string id)
        {
            await _applicationNotificationCollection.DeleteOneAsync(x => x.ApplicationNotificationId == id);
        }

        public async Task<List<ResultApplicationNotificationDto>> GetAllResultApplicationNotificationAsync()
        {
            var value = await _applicationNotificationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultApplicationNotificationDto>>(value);
        }


    }
}
