using Makaan.DtoLayer.CommentDtos.MemberCommentDtos;
using Makaan.WebUI.Services.MemberCommentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Client.ManagedCodeConventions;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MemberCommentController : Controller
    {
        private readonly IMemberCommentService _memberCommentService;

        public MemberCommentController(IMemberCommentService memberCommentService)
        {
            _memberCommentService = memberCommentService;
        }

        public async Task<IActionResult> Index(string? memberName,int page=1)
        {
            var values = await _memberCommentService.GetAllResultMemberCommentAsync();

            // Eğer propertyName varsa, filtreleme yapıyoruz
            if (!string.IsNullOrEmpty(memberName))
            {
                values = values.Where(x => x.MemberName.Contains(memberName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            int pageSize = 5;

            // Sayfalama işlemi
            var paginatedMemberComments = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // ViewData ile sayfa bilgilerini gönderiyoruz
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(values.Count / (double)pageSize);
            ViewData["pageSize"] = pageSize;

            if (values != null)
            {
                return View(paginatedMemberComments);
            }

            return View(new List<ResultMemberCommentDto>());
        }
        
        public async Task<IActionResult> UpdateMemberComment(string id)
        {
            var value = await _memberCommentService.GetByIdMemberCommentAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMemberComment(GetByIdMemberCommentDto getByIdMemberCommentDto, string CommentStatus)
        {
            getByIdMemberCommentDto.Status = CommentStatus == "Aktif" ? true : false;

            await _memberCommentService.UpdateMemberCommentAsync(getByIdMemberCommentDto);
            return Redirect("/Admin/MemberComment/Index");
        }

        public async Task<IActionResult> DeleteMemberComment(string id)
        {
            await _memberCommentService.DeleteMemberCommentAsync(id);
            return Redirect("/Admin/MemberComment/Index");
        }
    }
}
