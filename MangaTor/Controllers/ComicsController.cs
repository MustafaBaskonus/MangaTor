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
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ComicsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }


        public IActionResult Index([FromQuery] ComicRequestParameters comicRequest)
        {
            var comics = _serviceManager.ComicService.AllComics(false,comicRequest);
            var pagination = new Pagination()
            {
                CurrentPage = comicRequest.PageNumber,
                ItemsPerPage = comicRequest.PageSize,
                TotalItems = _serviceManager.ComicService.TotalComic(comicRequest),
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

            var comic = await _serviceManager.ComicService.OneComicWithChaptesAsync(comicSlug);
            commentRequest.ComicId = comic.ComicId;

            var comments = await _serviceManager.CommentService.GetAllCommentsDtoForComic(commentRequest);
            Pagination pagination = new Pagination()
            {
                CurrentPage = commentRequest.PageNumber,
                ItemsPerPage = commentRequest.PageSize,
                TotalItems = _serviceManager.CommentService.TotolComment(commentRequest),
            };
            var commentWithPaginations = new CommentListViewModel()
            {
                Comments = comments,
                Pagination = pagination
            };
            //score
            var averageScore = _serviceManager.RatingService.GetComicAverageScore(comic.ComicId);
            var userScore = await _serviceManager.RatingService.GetScoreByUser(comic.ComicId,this.HttpContext);
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
            var slug = await _serviceManager.CommentService.CreateCommentForComic(comicId, newComment,parentId, HttpContext);
            //return RedirectToAction("Details", new { comicSlug = slug });
            return RedirectToAction("Details","Comics",new { comicSlug = slug },fragment: "comments");
        }
    }
}