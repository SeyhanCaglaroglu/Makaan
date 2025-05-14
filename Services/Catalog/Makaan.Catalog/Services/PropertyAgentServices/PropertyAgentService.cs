using AutoMapper;
using Makaan.Catalog.Dtos.PropertyAgentDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.PropertyAgentServices
{
    public class PropertyAgentService : IPropertyAgentService
    {
        private readonly IMongoCollection<PropertyAgent> _propertyAgentCollection;
        private readonly IMapper _mapper;

        public PropertyAgentService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyAgentCollection = database.GetCollection<PropertyAgent>(_databaseSettings.PropertyAgentCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePropertyAgentAsync(CreatePropertyAgentDto createPropertyAgentDto)
        {
            var value = _mapper.Map<PropertyAgent>(createPropertyAgentDto);
            await _propertyAgentCollection.InsertOneAsync(value);
        }

        public async Task DeletePropertyAgentAsync(string id)
        {
            await _propertyAgentCollection.DeleteOneAsync(x => x.PropertyAgentId == id);
        }

        public async Task<List<ResultPropertyAgentDto>> GetAllResultPropertyAgentAsync()
        {
            var value = await _propertyAgentCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultPropertyAgentDto>>(value);
        }

        public async Task<GetByIdPropertyAgentDto> GetByIdPropertyAgentAsync(string id)
        {
            var value = await _propertyAgentCollection.Find<PropertyAgent>(x => x.PropertyAgentId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyAgentDto>(value);
        }

        public async Task<GetByIdPropertyAgentDto> GetPropertyAgentByTitleAsync(string title)
        {
            var value = await _propertyAgentCollection.Find<PropertyAgent>(x => x.Title == title).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyAgentDto>(value);
        }

        public async Task UpdatePropertyAgentAsync(UpdatePropertyAgentDto updatePropertyAgentDto)
        {
            var value = _mapper.Map<PropertyAgent>(updatePropertyAgentDto);

            await _propertyAgentCollection.FindOneAndReplaceAsync(x => x.PropertyAgentId == updatePropertyAgentDto.PropertyAgentId, value);
        }
    }
}
