using AutoMapper;
using Makaan.Catalog.Dtos.PropertyImageDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.PropertyImageServices
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IMongoCollection<PropertyImage> _propertyImageCollection;
        private readonly IMapper _mapper;

        public PropertyImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyImageCollection = database.GetCollection<PropertyImage>(_databaseSettings.PropertyImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePropertyImageAsync(CreatePropertyImageDto createPropertyImageDto)
        {
            var value = _mapper.Map<PropertyImage>(createPropertyImageDto);
            await _propertyImageCollection.InsertOneAsync(value);
        }

        public async Task DeletePropertyImageAsync(string id)
        {
            await _propertyImageCollection.DeleteOneAsync(x => x.PropertyImageId == id);
        }

        public async Task<List<ResultPropertyImageDto>> GetAllResultPropertyImageAsync()
        {
            var value = await _propertyImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultPropertyImageDto>>(value);
        }

        public async Task<GetByIdPropertyImageDto> GetByIdPropertyImageAsync(string id)
        {
            var value = await _propertyImageCollection.Find<PropertyImage>(x => x.PropertyImageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyImageDto>(value);
        }

        public async Task<List<ResultPropertyImageDto>> GetByPropertyIdPropertyImageAsync(string id)
        {
            var values = await _propertyImageCollection.Find<PropertyImage>(x => x.PropertyId == id).ToListAsync();
            return _mapper.Map<List<ResultPropertyImageDto>>(values);
        }

        public async Task UpdatePropertyImageAsync(UpdatePropertyImageDto updatePropertyImageDto)
        {
            var value = _mapper.Map<PropertyImage>(updatePropertyImageDto);

            await _propertyImageCollection.FindOneAndReplaceAsync(x => x.PropertyImageId == updatePropertyImageDto.PropertyImageId, value);
        }
    }
}
