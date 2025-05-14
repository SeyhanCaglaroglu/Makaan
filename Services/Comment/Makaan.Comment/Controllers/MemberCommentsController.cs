using Makaan.Comment.Dtos.MemberCommentDtos;
using Makaan.Comment.Services.MemberCommentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberCommentsController : ControllerBase
    {
        private readonly IMemberCommentService _memberCommentService;

        public MemberCommentsController(IMemberCommentService MemberCommentService)
        {
            _memberCommentService = MemberCommentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMemberComment()
        {
            var value = await _memberCommentService.GetAllResultMemberCommentAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMemberComment(CreateMemberCommentDto createMemberCommentDto)
        {

            await _memberCommentService.CreateMemberCommentAsync(createMemberCommentDto);

            return Ok("Yorum Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMemberComment(UpdateMemberCommentDto updateMemberCommentDto)
        {
            await _memberCommentService.UpdateMemberCommentAsync(updateMemberCommentDto);
            return Ok("Yorum Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMemberComment(string id)
        {
            await _memberCommentService.DeleteMemberCommentAsync(id);
            return Ok("Yorum Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdMemberComment(string id)
        {
            var value = await _memberCommentService.GetByIdMemberCommentAsync(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveMemberComment()
        {
            var value = await _memberCommentService.GetAllActiveMemberCommentAsync();
            return Ok(value);
        }
    }
}
