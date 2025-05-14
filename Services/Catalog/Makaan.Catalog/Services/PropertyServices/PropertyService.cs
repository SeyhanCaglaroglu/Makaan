using AutoMapper;
using Makaan.Catalog.Dtos.PropertyDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.PropertyServices
{
    public class PropertyService : IPropertyService
    {
        private readonly IMongoCollection<Property> _propertyCollection;
        private readonly IMapper _mapper;

        public PropertyService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyCollection = database.GetCollection<Property>(_databaseSettings.PropertyCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePropertyAsync(CreatePropertyDto createPropertyDto)
        {
            var value = _mapper.Map<Property>(createPropertyDto);
            await _propertyCollection.InsertOneAsync(value);
        }

        public async Task DeletePropertyAsync(string id)
        {
            await _propertyCollection.DeleteOneAsync(x => x.PropertyId == id);
        }

        public async Task<List<ResultPropertyDto>> GetAllResultPropertyAsync()
        {
            var value = await _propertyCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultPropertyDto>>(value);
        }

        public async Task<GetByIdPropertyDto> GetByIdPropertyAsync(string id)
        {
            var value = await _propertyCollection.Find<Property>(x => x.PropertyId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyDto>(value);
        }

        public async Task<List<ResultPropertyDto>> GetPropertiesByPropertyAgentId(string id)
        {
            var value = await _propertyCollection.Find(x => x.PropertyAgentId == id).ToListAsync();

            return _mapper.Map<List<ResultPropertyDto>>(value);
        }

        public async Task<List<ResultPropertyDto>> GetPropertiesByPropertyTypeId(string id)
        {
            var value = await _propertyCollection.Find(x=>x.PropertyTypeId == id).ToListAsync();

            return _mapper.Map<List<ResultPropertyDto>>(value);
        }

        public async Task<int> GetPropertyCountByPropertyTypeId(string id)
        {
            var values = await _propertyCollection.Find(x=>x.PropertyTypeId == id).ToListAsync();
            return values.Count;
        }

        public async Task UpdatePropertyAsync(UpdatePropertyDto updatePropertyDto)
        {
            var value = _mapper.Map<Property>(updatePropertyDto);

            await _propertyCollection.FindOneAndReplaceAsync(x => x.PropertyId == updatePropertyDto.PropertyId, value);
        }
    }
}
