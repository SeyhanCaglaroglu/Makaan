using AutoMapper;
using Makaan.Comment.Dtos.MemberCommentDtos;
using Makaan.Comment.Entities;

namespace Makaan.Comment.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<MemberComment,ResultMemberCommentDto>().ReverseMap();
            CreateMap<MemberComment,CreateMemberCommentDto>().ReverseMap();
            CreateMap<MemberComment,UpdateMemberCommentDto>().ReverseMap();
            CreateMap<MemberComment,GetByIdMemberCommentDto>().ReverseMap();
        }
    }
}
