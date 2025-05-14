using AutoMapper;
using Makaan.Catalog.Dtos.IntroTextAreaDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.IntroTextAreaServices
{
    public class IntroTextAreaService : IIntroTextAreaService
    {
        private readonly IMongoCollection<IntroTextArea> _introTextAreaCollection;
        private readonly IMapper _mapper;

        public IntroTextAreaService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _introTextAreaCollection = database.GetCollection<IntroTextArea>(_databaseSettings.IntroTextAreaCollectionName);
            _mapper = mapper;
        }

        public async Task CreateIntroTextAreaAsync(CreateIntroTextAreaDto createIntroTextAreaDto)
        {
            

            var value = _mapper.Map<IntroTextArea>(createIntroTextAreaDto);

            try
            {
                await _introTextAreaCollection.InsertOneAsync(value);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while creating the intro text area.", ex);
            }
            
        }

        public async Task DeleteIntroTextAreaAsync(string id)
        {
            

            try
            {
                var result = await _introTextAreaCollection.DeleteOneAsync(x => x.IntroTextAreaId == id);

                if (result.DeletedCount == 0)
                {
                    throw new KeyNotFoundException($"No intro text area found with ID: {id}");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while delete the intro text area.", ex);
            }

            
        }

        public async Task<List<ResultIntroTextAreaDto>> GetAllIntroTextAreaAsync()
        {
            var value = await _introTextAreaCollection.Find(x=>true).ToListAsync();

            return _mapper.Map<List<ResultIntroTextAreaDto>>(value);
        }

        public async Task<GetByIdIntroTextAreaDto> GetByIdIntroTextAreaAsync(string id)
        {
            

            try
            {
                var value = await _introTextAreaCollection.Find<IntroTextArea>(x => x.IntroTextAreaId == id).FirstOrDefaultAsync();

                return _mapper.Map<GetByIdIntroTextAreaDto>(value);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving the intro text area.", ex);
            }
        }

        public async Task UpdateIntroTextAreaAsync(UpdateIntroTextAreaDto updateIntroTextAreaDto)
        {
            

            try
            {
                var value = _mapper.Map<IntroTextArea>(updateIntroTextAreaDto);

                var result = await _introTextAreaCollection.FindOneAndReplaceAsync(x => x.IntroTextAreaId == updateIntroTextAreaDto.IntroTextAreaId, value);

                if (result == null)
                {
                    throw new KeyNotFoundException($"No intro text area found with ID: {updateIntroTextAreaDto.IntroTextAreaId}");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the intro text area.", ex);
            }

            
        }
    }
}
