using AutoMapper;
using Makaan.Catalog.Dtos.PropertyIntroPosterDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.PropertyIntroPosterServices
{
    public class PropertyIntroPosterService : IPropertyIntroPosterService
    {
        private readonly IMongoCollection<PropertyIntroPoster> _propertyIntroPosterCollection;
        private readonly IMapper _mapper;

        public PropertyIntroPosterService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyIntroPosterCollection = database.GetCollection<PropertyIntroPoster>(_databaseSettings.PropertyIntroPosterCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePropertyIntroPosterAsync(CreatePropertyIntroPosterDto createPropertyIntroPosterDto)
        {
            var value = _mapper.Map<PropertyIntroPoster>(createPropertyIntroPosterDto);
            await _propertyIntroPosterCollection.InsertOneAsync(value);
        }

        public async Task DeletePropertyIntroPosterAsync(string id)
        {
            await _propertyIntroPosterCollection.DeleteOneAsync(x => x.PropertyIntroPosterId == id);
        }

        public async Task<List<ResultPropertyIntroPosterDto>> GetAllResultPropertyIntroPosterAsync()
        {
            var value = await _propertyIntroPosterCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultPropertyIntroPosterDto>>(value);
        }

        public async Task<GetByIdPropertyIntroPosterDto> GetByIdPropertyIntroPosterAsync(string id)
        {
            var value = await _propertyIntroPosterCollection.Find<PropertyIntroPoster>(x => x.PropertyIntroPosterId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyIntroPosterDto>(value);
        }

        public async Task UpdatePropertyIntroPosterAsync(UpdatePropertyIntroPosterDto updatePropertyIntroPosterDto)
        {
            var value = _mapper.Map<PropertyIntroPoster>(updatePropertyIntroPosterDto);

            await _propertyIntroPosterCollection.FindOneAndReplaceAsync(x => x.PropertyIntroPosterId == updatePropertyIntroPosterDto.PropertyIntroPosterId, value);
        }
    }
}
