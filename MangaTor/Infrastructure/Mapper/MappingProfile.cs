using AutoMapper;
using DAL.Entities;
using DAL.Entities.DTOs;
using MangaTor.Models;
using Microsoft.AspNetCore.Identity;

namespace MangaTor.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comic, AdminComicDto>().ReverseMap();
            CreateMap<Category, CategoryWithTotalChapteDto>()
                                .ForMember(dest => dest.TotalComic,
                                 opt => opt.MapFrom(src => src.Comics.Count));
            CreateMap<UserDtoForInsertion, IdentityUser>();
            CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap();
            CreateMap<Comic, ComicDetailViewModel>();

            //reaction
            CreateMap<ReactionType, ReactionTypeDto>();




            // Comment -> CommentDtoForDetail
            CreateMap<Comment, CommentDtoForDetail>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ComicTitle,
                           opt => opt.MapFrom(src => src.Comic != null ? src.Comic.Title : null))
                .ForMember(dest => dest.ChapterTitle,
                           opt => opt.MapFrom(src => src.Chapter != null ? src.Chapter.Title : null))
                .ForMember(dest => dest.CommentUserName,
                           opt => opt.MapFrom(src => src.ParentComment != null ? src.ParentComment.User.UserName : null))
                .ForMember(dest => dest.Replies,
                           opt => opt.MapFrom(src => src.Replies));

            // Comment -> ReplyDto (çünkü Reply entity'si de aslında Comment'ten türeyen kayıt)
            CreateMap<Comment, ReplyDto>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ReplyText,
                            opt => opt.MapFrom(src => src.Content ?? null));


        }
    }
}
