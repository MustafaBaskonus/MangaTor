using Microsoft.AspNetCore.Mvc;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using DAL.Entities.RequestParameters;
using MangaTor.Models;
using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace MangaTor.Controllers
{
    public class ComicsController : Controller
    {
        private readonly IServiceManager _services;
     

        public ComicsController(IServiceManager services)
        {
            _services = services;
        }


        public IActionResult Index([FromQuery] ComicRequestParameters comicRequest)
        {
            var comics = _services.ComicService.AllComics(false,comicRequest);
            var pagination = new Pagination()
            {
                CurrentPage = comicRequest.PageNumber,
                ItemsPerPage = comicRequest.PageSize,
                TotalItems = _services.ComicService.TotalComic(comicRequest),
            };
            return View(new ComicListViewModel()
            {
                Comics = comics,
                Pagination = pagination
            });

        }


        [HttpGet("Comics/{comicSlug}")]
        public async Task<IActionResult> Details(string comicSlug, CommentRequestParameters commentRequest)
        {
            if (string.IsNullOrEmpty(comicSlug))
            {
                return NotFound();
            }

            var comic = await _services.ComicService.OneComicWithChaptesAsync(comicSlug);
            commentRequest.ComicId = comic.ComicId;

            var comments = await _services.CommentService.GetAllCommentsDtoForComic(commentRequest);
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
            //score
            var averageScore = _services.RatingService.GetComicAverageScore(comic.ComicId);
            var userScore = await _services.RatingService.GetScoreByUser(comic.ComicId,this.HttpContext);
            var comicDetailDto = new ComicDetailViewModel()
            {
                Comic = comic,
                Chapters = comic.Chapters.ToList(),
                commentWithPaginations = commentWithPaginations,
                AverageScore = averageScore,
                UserScore= userScore
            };

            if (comic == null)
            {
                return NotFound();
            }

            return View(comicDetailDto);
        }


        [HttpPost]
        public async Task<IActionResult >AddComment(int comicId, string newComment,int? parentId)
        {
            var slug = await _services.CommentService.CreateCommentForComic(comicId, newComment,parentId, HttpContext);
            //return RedirectToAction("Details", new { comicSlug = slug });
            return RedirectToAction("Details","Comics",new { comicSlug = slug },fragment: "comments");
        }
    }
}