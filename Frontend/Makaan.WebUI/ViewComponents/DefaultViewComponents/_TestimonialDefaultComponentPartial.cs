using Makaan.DtoLayer.CommentDtos.MemberCommentDtos;
using Makaan.WebUI.Services.MemberCommentServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _TestimonialDefaultComponentPartial : ViewComponent
    {
        private readonly IMemberCommentService _memberCommentService;

        public _TestimonialDefaultComponentPartial(IMemberCommentService memberCommentService)
        {
            _memberCommentService = memberCommentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var comments = await _memberCommentService.GetAllActiveMemberCommentAsync();

            return View(comments ?? new List<ResultMemberCommentDto>());
        }
    
    }
}
