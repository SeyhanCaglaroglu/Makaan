using AutoMapper;
using Makaan.Catalog.Dtos.PropertyDetailDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.PropertyDetailServices
{
    public class PropertyDetailService : IPropertyDetailService
    {
        private readonly IMongoCollection<PropertyDetail> _propertyDetailCollection;
        private readonly IMapper _mapper;

        public PropertyDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyDetailCollection = database.GetCollection<PropertyDetail>(_databaseSettings.PropertyDetailCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePropertyDetailAsync(CreatePropertyDetailDto createPropertyDetailDto)
        {
            var value = _mapper.Map<PropertyDetail>(createPropertyDetailDto);
            await _propertyDetailCollection.InsertOneAsync(value);
        }

        public async Task DeletePropertyDetailAsync(string id)
        {
            await _propertyDetailCollection.DeleteOneAsync(x => x.PropertyDetailId == id);
        }

        public async Task<List<ResultPropertyDetailDto>> GetAllResultPropertyDetailAsync()
        {
            var value = await _propertyDetailCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultPropertyDetailDto>>(value);
        }

        public async Task<GetByIdPropertyDetailDto> GetByIdPropertyDetailAsync(string id)
        {
            var value = await _propertyDetailCollection.Find<PropertyDetail>(x => x.PropertyDetailId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPropertyDetailDto>(value);
        }

        public async Task<List<ResultPropertyDetailDto>> GetByPropertyIdPropertyDetailAsync(string id)
        {
            var values = await _propertyDetailCollection.Find<PropertyDetail>(x=>x.PropertyId == id).ToListAsync();
            return _mapper.Map<List<ResultPropertyDetailDto>>(values);
        }

        public async Task UpdatePropertyDetailAsync(UpdatePropertyDetailDto updatePropertyDetailDto)
        {
            var value = _mapper.Map<PropertyDetail>(updatePropertyDetailDto);

            await _propertyDetailCollection.FindOneAndReplaceAsync(x => x.PropertyDetailId == updatePropertyDetailDto.PropertyDetailId, value);
        }
    }
}
