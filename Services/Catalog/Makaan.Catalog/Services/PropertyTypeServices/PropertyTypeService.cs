using AutoMapper;
using Makaan.Catalog.Dtos.PropertyTypeDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.PropertyTypeServices
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IMongoCollection<PropertyType> _propertyTypeCollection;
        private readonly IMapper _mapper;

        public PropertyTypeService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyTypeCollection = database.GetCollection<PropertyType>(_databaseSettings.PropertyTypeCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto)
        {
            var value = _mapper.Map<PropertyType>(createPropertyTypeDto);
            await _propertyTypeCollection.InsertOneAsync(value);
        }

        public async Task DeletePropertyTypeAsync(string id)
        {
            await _propertyTypeCollection.DeleteOneAsync(x => x.PropertyTypeId == id);
        }

        public async Task<List<ResultPropertyTypeDto>> GetAllResultPropertyTypeAsync()
        {
            var value = await _propertyTypeCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultPropertyTypeDto>>(value);
        }

        public async Task<GetByIdPropertyTypeDto> GetByIdPropertyTypeAsync(string id)
        {
            var value = await _propertyTypeCollection.Find<PropertyType>(x => x.PropertyTypeId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyTypeDto>(value);
        }

        public async Task UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto)
        {
            var value = _mapper.Map<PropertyType>(updatePropertyTypeDto);

            await _propertyTypeCollection.FindOneAndReplaceAsync(x => x.PropertyTypeId == updatePropertyTypeDto.PropertyTypeId, value);
        }
    }
}
