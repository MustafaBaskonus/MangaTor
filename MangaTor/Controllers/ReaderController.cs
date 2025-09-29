using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using Services.Contacts;
using MangaTor.Models;
using DAL.Entities.RequestParameters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MangaTor.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IServiceManager _services;

        public ReaderController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet("Read/{comicSlug}/{chapterNo}")]
        public async Task<IActionResult> Read(string comicSlug, int chapterNo, CommentRequestParameters commentRequest)
        {
            if (string.IsNullOrEmpty(comicSlug) || chapterNo < 0)
            {
                return NotFound();
            }

            var chapter = await _services.ChapterService.FindChapterwithComicAndPagesAsync(chapterNo, comicSlug);

            if (chapter == null)
            {
                return NotFound();
            }


            var allChaptersInSeries = await _services.ChapterService.AllChaptersInSeries(chapter.ChapterId);
                                                  
            var currentIndex = allChaptersInSeries.FindIndex(c => c.ChapterId == chapter.ChapterId);


            if (currentIndex > 0)
            {
                ViewBag.PreviousChapterNo = allChaptersInSeries[currentIndex - 1].ChapterNo;
            }

            if (currentIndex < allChaptersInSeries.Count - 1)
            {
                ViewBag.NextChapterNo = allChaptersInSeries[currentIndex + 1].ChapterNo;
            }

            ViewBag.ComicSlug = comicSlug;


            //paination for comments
            commentRequest.ChapterId = chapter.ChapterId;
            var comments = await _services.CommentService.GetAllCommentsDtoForChapter(commentRequest);
            Pagination pagination = new Pagination()
            {
                CurrentPage = commentRequest.PageNumber,
                ItemsPerPage = commentRequest.PageSize,
                TotalItems = _services.CommentService.TotolComment(commentRequest),
            };
            var commentWithPaginations = new CommentListViewModel()
            {
                Comments = comments,
                Pagination = pagination
            };


            ChapterWithCommentDto chapterDto = new ChapterWithCommentDto()
            {
                Chapter=chapter,
                commentWithPaginations = commentWithPaginations
            };

            return View(chapterDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int chapterId, string newComment, int? parentId)
        {
            var chapter = await _services.CommentService.CreateCommentForChapter(chapterId, newComment, parentId, HttpContext);
            //return RedirectToAction("Read", new { comicSlug = chapter.Comic.Slug, chapterNo = chapter.ChapterNo });
            return RedirectToAction("Read", "Reader", new { comicSlug = chapter.Comic.Slug, chapterNo = chapter.ChapterNo }, fragment: "comments");
        }
    }
}