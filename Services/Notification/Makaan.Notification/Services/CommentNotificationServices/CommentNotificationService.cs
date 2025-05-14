using AutoMapper;
using Makaan.Notification.Dtos.CommentNotificationDtos;
using Makaan.Notification.Entities;
using Makaan.Notification.Settings;
using MongoDB.Driver;

namespace Makaan.Notification.Services.CommentNotificationServices
{
    public class CommentNotificationService : ICommentNotificationService
    {
        private readonly IMongoCollection<CommentNotification> _commentNotificationCollection;
        private readonly IMapper _mapper;

        public CommentNotificationService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _commentNotificationCollection = database.GetCollection<CommentNotification>(_databaseSettings.CommentNotificationCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCommentNotificationAsync(CreateCommentNotificationDto createCommentNotificationDto)
        {
            var value = _mapper.Map<CommentNotification>(createCommentNotificationDto);
            await _commentNotificationCollection.InsertOneAsync(value);
        }

        public async Task DeleteAllCommentNotificationAsync()
        {
            await _commentNotificationCollection.DeleteManyAsync(FilterDefinition<CommentNotification>.Empty);
        }

        public async Task DeleteCommentNotificationAsync(string id)
        {
            await _commentNotificationCollection.DeleteOneAsync(x => x.CommentNotificationId == id);
        }

        public async Task<List<ResultCommentNotificationDto>> GetAllResultCommentNotificationAsync()
        {
            var value = await _commentNotificationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCommentNotificationDto>>(value);
        }

        
    }
}
