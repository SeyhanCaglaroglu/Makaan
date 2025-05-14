using AutoMapper;
using Makaan.Catalog.Dtos.IntroSliderAreaDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.IntroSliderAreaServices
{
    public class IntroSliderAreaService : IIntroSliderAreaService
    {
        private readonly IMongoCollection<IntroSliderArea> _introSliderAreaCollection;
        private readonly IMapper _mapper;

        public IntroSliderAreaService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _introSliderAreaCollection = database.GetCollection<IntroSliderArea>(_databaseSettings.IntroSliderAreaCollectionName);
            _mapper = mapper;
        }

        public async Task CreateIntroSliderAreaAsync(CreateIntroSliderAreaDto createIntroSliderAreaDto)
        {
            var value = _mapper.Map<IntroSliderArea>(createIntroSliderAreaDto);
            await _introSliderAreaCollection.InsertOneAsync(value);
        }

        public async Task DeleteIntroSliderAreaAsync(string id)
        {
            await _introSliderAreaCollection.DeleteOneAsync(x=>x.IntroSliderAreaId == id);
        }

        public async Task<List<ResultIntroSliderAreaDto>> GetAllResultIntroSliderAreaAsync()
        {
            var value = await _introSliderAreaCollection.Find(x=>true).ToListAsync();
            return _mapper.Map<List<ResultIntroSliderAreaDto>>(value);
        }

        public async Task<GetByIdIntroSliderAreaDto> GetByIdIntroSliderAreaAsync(string id)
        {
            var value = await _introSliderAreaCollection.Find<IntroSliderArea>(x=>x.IntroSliderAreaId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdIntroSliderAreaDto>(value);
        }

        public async Task UpdateIntroSliderAreaAsync(UpdateIntroSliderAreaDto updateIntroSliderAreaDto)
        {
            var value = _mapper.Map<IntroSliderArea>(updateIntroSliderAreaDto);

            await _introSliderAreaCollection.FindOneAndReplaceAsync(x => x.IntroSliderAreaId == updateIntroSliderAreaDto.IntroSliderAreaId, value);
        }
    }
}
