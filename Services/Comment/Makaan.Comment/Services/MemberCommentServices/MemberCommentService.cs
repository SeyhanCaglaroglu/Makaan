using AutoMapper;
using Makaan.Comment.Dtos.MemberCommentDtos;
using Makaan.Comment.Entities;
using Makaan.Comment.Settings;
using MongoDB.Driver;

namespace Makaan.Comment.Services.MemberCommentServices
{
    public class MemberCommentService : IMemberCommentService
    {
        private readonly IMongoCollection<MemberComment> _memberCommentCollection;
        private readonly IMapper _mapper;

        public MemberCommentService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _memberCommentCollection = database.GetCollection<MemberComment>(_databaseSettings.MemberCommentCollectionName);
            _mapper = mapper;
        }

        public async Task CreateMemberCommentAsync(CreateMemberCommentDto createMemberCommentDto)
        {
            var value = _mapper.Map<MemberComment>(createMemberCommentDto);
            await _memberCommentCollection.InsertOneAsync(value);
        }

        public async Task DeleteMemberCommentAsync(string id)
        {
            await _memberCommentCollection.DeleteOneAsync(x => x.MemberCommentId == id);
        }

        public async Task<List<ResultMemberCommentDto>> GetAllActiveMemberCommentAsync()
        {
            var value = await _memberCommentCollection.Find<MemberComment>(x=>x.Status == true).ToListAsync();
            return _mapper.Map<List<ResultMemberCommentDto>>(value);
        }

        public async Task<List<ResultMemberCommentDto>> GetAllResultMemberCommentAsync()
        {
            var value = await _memberCommentCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultMemberCommentDto>>(value);
        }

        public async Task<GetByIdMemberCommentDto> GetByIdMemberCommentAsync(string id)
        {
            var value = await _memberCommentCollection.Find<MemberComment>(x => x.MemberCommentId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdMemberCommentDto>(value);
        }

        public async Task UpdateMemberCommentAsync(UpdateMemberCommentDto updateMemberCommentDto)
        {
            var value = _mapper.Map<MemberComment>(updateMemberCommentDto);

            await _memberCommentCollection.FindOneAndReplaceAsync(x => x.MemberCommentId == updateMemberCommentDto.MemberCommentId, value);
        }
    }
}
