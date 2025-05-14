using Makaan.DtoLayer.CommentDtos.MemberCommentDtos;

namespace Makaan.WebUI.Services.MemberCommentServices
{
    public interface IMemberCommentService
    {
        Task<List<ResultMemberCommentDto>> GetAllResultMemberCommentAsync();
        Task CreateMemberCommentAsync(CreateMemberCommentDto createMemberCommentDto);
        Task UpdateMemberCommentAsync(GetByIdMemberCommentDto getByIdMemberCommentDto);
        Task DeleteMemberCommentAsync(string id);
        Task<GetByIdMemberCommentDto> GetByIdMemberCommentAsync(string id);
        Task<List<ResultMemberCommentDto>> GetAllActiveMemberCommentAsync();
    }
}
