using AutoMapper;
using Makaan.Catalog.Dtos.EstateAgentApplicationDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.EstateAgentApplicationServices
{
    public class EstateAgentApplicationService : IEstateAgentApplicationService
    {
        private readonly IMongoCollection<EstateAgentApplication> _estateAgentApplicationCollection;
        private readonly IMapper _mapper;

        public EstateAgentApplicationService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _estateAgentApplicationCollection = database.GetCollection<EstateAgentApplication>(_databaseSettings.EstateAgentApplicationCollectionName);
            _mapper = mapper;
        }

        public async Task CreateEstateAgentApplicationAsync(CreateEstateAgentApplicationDto createEstateAgentApplicationDto)
        {
            var value = _mapper.Map<EstateAgentApplication>(createEstateAgentApplicationDto);
            await _estateAgentApplicationCollection.InsertOneAsync(value);
        }

        public async Task DeleteEstateAgentApplicationAsync(string id)
        {
            await _estateAgentApplicationCollection.DeleteOneAsync(x => x.EstateAgentApplicationId == id);
        }

        public async Task<List<ResultEstateAgentApplicationDto>> GetAllResultEstateAgentApplicationAsync()
        {
            var value = await _estateAgentApplicationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultEstateAgentApplicationDto>>(value);
        }

        public async Task<GetByIdEstateAgentApplicationDto> GetByIdEstateAgentApplicationAsync(string id)
        {
            var value = await _estateAgentApplicationCollection.Find<EstateAgentApplication>(x => x.EstateAgentApplicationId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdEstateAgentApplicationDto>(value);
        }

        public async Task<GetByIdEstateAgentApplicationDto> GetEstateAgentApplicationByAuthNameAsync(string authName)
        {
            var value = await _estateAgentApplicationCollection.Find<EstateAgentApplication>(x => x.AuthorizedNameSurname == authName).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdEstateAgentApplicationDto>(value);
        }
    }
}
