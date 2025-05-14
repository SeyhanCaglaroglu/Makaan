using AutoMapper;
using Makaan.Catalog.Dtos.FeaturedAboutDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.FeaturedAboutServices
{
    public class FeaturedAboutService : IFeaturedAboutService
    {
        private readonly IMongoCollection<FeaturedAbout> _featuredAboutCollection;
        private readonly IMapper _mapper;

        public FeaturedAboutService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featuredAboutCollection = database.GetCollection<FeaturedAbout>(_databaseSettings.FeaturedAboutCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeaturedAboutAsync(CreateFeaturedAboutDto createFeaturedAboutDto)
        {
            var value = _mapper.Map<FeaturedAbout>(createFeaturedAboutDto);
            await _featuredAboutCollection.InsertOneAsync(value);
        }

        public async Task DeleteFeaturedAboutAsync(string id)
        {
            await _featuredAboutCollection.DeleteOneAsync(x => x.FeaturedAboutId == id);
        }

        public async Task<List<ResultFeaturedAboutDto>> GetAllResultFeaturedAboutAsync()
        {
            var value = await _featuredAboutCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeaturedAboutDto>>(value);
        }

        public async Task<GetByIdFeaturedAboutDto> GetByIdFeaturedAboutAsync(string id)
        {
            var value = await _featuredAboutCollection.Find<FeaturedAbout>(x => x.FeaturedAboutId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeaturedAboutDto>(value);
        }

        public async Task UpdateFeaturedAboutAsync(UpdateFeaturedAboutDto updateFeaturedAboutDto)
        {
            var value = _mapper.Map<FeaturedAbout>(updateFeaturedAboutDto);

            await _featuredAboutCollection.FindOneAndReplaceAsync(x => x.FeaturedAboutId == updateFeaturedAboutDto.FeaturedAboutId, value);
        }
    }
}
