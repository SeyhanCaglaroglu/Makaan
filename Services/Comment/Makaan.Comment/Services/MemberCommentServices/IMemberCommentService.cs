using Makaan.Comment.Dtos.MemberCommentDtos;

namespace Makaan.Comment.Services.MemberCommentServices
{
    public interface IMemberCommentService
    {
        Task<List<ResultMemberCommentDto>> GetAllResultMemberCommentAsync();
        Task CreateMemberCommentAsync(CreateMemberCommentDto createMemberCommentDto);
        Task UpdateMemberCommentAsync(UpdateMemberCommentDto updateMemberCommentDto);
        Task DeleteMemberCommentAsync(string id);
        Task<GetByIdMemberCommentDto> GetByIdMemberCommentAsync(string id);
        Task<List<ResultMemberCommentDto>> GetAllActiveMemberCommentAsync();
    }
}
