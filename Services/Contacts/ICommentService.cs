using DAL.Entities;
using DAL.Entities.DTOs;
using DAL.Entities.RequestParameters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface ICommentService 
    {
        //Comment Add for comic
        Task<string> CreateCommentForComic(int comicId, string newComment, int? parentId, HttpContext httpContext);
        //Comment Add for chapter
        Task<Chapter> CreateCommentForChapter(int chapterId, string newComment, int? parentId, HttpContext httpContext);
        Task Delete(int? commentId);


        Task<List<CommentDtoForDetail>> GetAllCommentsDto(CommentRequestParameters commentRequest);
        Task<List<CommentDtoForDetail>> GetAllCommentsDtoForChapter(CommentRequestParameters commentRequest);
        Task<List<CommentDtoForDetail>> GetAllCommentsDtoForComic(CommentRequestParameters commentRequest);
        //toplam yorum
        int TotolComment(CommentRequestParameters commentRequest);
    }
}
