using AutoMapper;
using Makaan.Catalog.Dtos.ContactIntroPosterDtos;
using Makaan.Catalog.Entites;
using Makaan.Catalog.Settings;
using MongoDB.Driver;

namespace Makaan.Catalog.Services.ContactIntroPosterServices
{
    public class ContactIntroPosterService : IContactIntroPosterService
    {
        private readonly IMongoCollection<ContactIntroPoster> _contactIntroPosterCollection;
        private readonly IMapper _mapper;

        public ContactIntroPosterService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _contactIntroPosterCollection = database.GetCollection<ContactIntroPoster>(_databaseSettings.ContactIntroPosterCollectionName);
            _mapper = mapper;
        }

        public async Task CreateContactIntroPosterAsync(CreateContactIntroPosterDto createContactIntroPosterDto)
        {
            var value = _mapper.Map<ContactIntroPoster>(createContactIntroPosterDto);
            await _contactIntroPosterCollection.InsertOneAsync(value);
        }

        public async Task DeleteContactIntroPosterAsync(string id)
        {
            await _contactIntroPosterCollection.DeleteOneAsync(x => x.ContactIntroPosterId == id);
        }

        public async Task<List<ResultContactIntroPosterDto>> GetAllResultContactIntroPosterAsync()
        {
            var value = await _contactIntroPosterCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultContactIntroPosterDto>>(value);
        }

        public async Task<GetByIdContactIntroPosterDto> GetByIdContactIntroPosterAsync(string id)
        {
            var value = await _contactIntroPosterCollection.Find<ContactIntroPoster>(x => x.ContactIntroPosterId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdContactIntroPosterDto>(value);
        }

        public async Task UpdateContactIntroPosterAsync(UpdateContactIntroPosterDto updateContactIntroPosterDto)
        {
            var value = _mapper.Map<ContactIntroPoster>(updateContactIntroPosterDto);

            await _contactIntroPosterCollection.FindOneAndReplaceAsync(x => x.ContactIntroPosterId == updateContactIntroPosterDto.ContactIntroPosterId, value);
        }
    }
}
