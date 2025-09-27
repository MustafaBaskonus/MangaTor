using AutoMapper;
using DAL.Context;
using DAL.Entities;
using DAL.Entities.DTOs;
using DAL.Entities.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommentManager: ICommentService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CommentManager(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = appDbContext;
            _userManager = userManager;
            _mapper = mapper;
        }






        public async Task<string> CreateCommentForComic(int comicId, string newComment, int? parentId, HttpContext httpContext)
        {
            var userId = _userManager.GetUserId(httpContext.User);

            var comment = new Comment
            {
                Content = newComment,
                ComicId = comicId,
                UserId = userId,
                ParentCommentId = parentId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var comic = await _context.Comics.FindAsync(comicId);
            return comic.Slug;
        }
        public async Task<Chapter> CreateCommentForChapter(int chapterId, string newComment, int? parentId, HttpContext httpContext)
        {
            var userId = _userManager.GetUserId(httpContext.User);

            var comment = new Comment
            {
                Content = newComment,
                ChapterId = chapterId,
                UserId = userId,
                ParentCommentId = parentId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var chapter = await _context.Chapters.Include(m => m.Comic).FirstOrDefaultAsync(m => m.ChapterId == chapterId);
            return chapter;
        }





        public async Task<List<CommentDtoForDetail>> GetAllCommentsDto(CommentRequestParameters commentRequest)
        {
            commentRequest.PageSize = 10;
            var comments = await _context.Comments.Include(m => m.User).Include(m => m.Comic).Include(m => m.Chapter)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((commentRequest.PageNumber - 1) * commentRequest.PageSize)
                .Take(commentRequest.PageSize).ToListAsync();

            var commentsDto =  _mapper.Map<List<CommentDtoForDetail>>(comments);
            return commentsDto;
        }
        public async Task<List<CommentDtoForDetail>> GetAllCommentsDtoForChapter(CommentRequestParameters commentRequest)
        {
            commentRequest.PageSize = 10;
            var comments = await _context.Comments.Include(m => m.User).Include(m => m.Comic).Include(m => m.Chapter)
                .Where(m=> m.ChapterId==commentRequest.ChapterId)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((commentRequest.PageNumber - 1) * commentRequest.PageSize)
                .Take(commentRequest.PageSize).ToListAsync();

            var commentsDto = _mapper.Map<List<CommentDtoForDetail>>(comments);
            return commentsDto;
        }
        public async Task<List<CommentDtoForDetail>> GetAllCommentsDtoForComic(CommentRequestParameters commentRequest)
        {
            commentRequest.PageSize = 10;
            var comments = await _context.Comments.Include(m => m.User).Include(m => m.Comic).Include(m => m.Chapter)
                .Where(m => m.ComicId == commentRequest.ComicId)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((commentRequest.PageNumber - 1) * commentRequest.PageSize)
                .Take(commentRequest.PageSize).ToListAsync();

            var commentsDto = _mapper.Map<List<CommentDtoForDetail>>(comments);
            return commentsDto;
        }



        public int TotolComment(CommentRequestParameters commentRequest)
        {
            var totalComments =  _context.Comments
                .Where(m=>m.ComicId==commentRequest.ComicId &&  m.ChapterId== commentRequest.ChapterId)
                .Count();

            return totalComments;
        }





        public async Task Delete(int? commentId)
        {
            if (commentId == null) return;

            var comment = await _context.Comments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment != null)
            {
                // Recursive silme
                await DeleteReplies(comment);

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        private async Task DeleteReplies(Comment comment)
        {
            if (comment.Replies != null && comment.Replies.Any())
            {
                foreach (var reply in comment.Replies.ToList())
                {
                    var child = await _context.Comments
                        .Include(c => c.Replies)
                        .FirstOrDefaultAsync(c => c.Id == reply.Id);

                    if (child != null)
                    {
                        await DeleteReplies(child);
                        _context.Comments.Remove(child);
                    }
                }
            }
        }
    }
}
