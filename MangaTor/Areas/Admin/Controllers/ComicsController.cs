using AutoMapper;
using DAL.Context;
using DAL.Entities;
using DAL.Entities.DTOs;
using DAL.Entities.RequestParameters;
using MangaTor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
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
            var comics = _serviceManager.ComicService.AllComicsAndChapters(false, comicRequest);
            Pagination pagination = new Pagination()
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

        [HttpGet("Admin/Comics/ComicCreate")]
        public async Task<IActionResult >ComicCreate()
        {
            var categories = await _serviceManager.CategoryService.GetAllCategoriesAsync(false);

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComicCreate([FromForm] AdminComicDto comicDto, IFormFile file)
        {
            await _serviceManager.ComicService.ComicCreate(comicDto, file);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comic = await _serviceManager.ComicService.FindComicWithId(id);
            var categories = await _serviceManager.CategoryService.GetAllCategoriesAsync(false);

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            AdminComicDto comicDto =_mapper.Map<AdminComicDto>(comic);

            return View(comicDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] AdminComicDto comicDto, IFormFile? file)
        {
            await _serviceManager.ComicService.ComicUpdate(comicDto, file);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.ComicService.ComicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Admin/Comics/{comicSlug}")]
        public async Task<IActionResult> Details(string comicSlug, [FromQuery] ChapterRequestParameters chapterRequest)
        {
            if (string.IsNullOrEmpty(comicSlug))
            {
                return NotFound();
            }
            var comic = await _serviceManager.ComicService.FindComic(comicSlug);
            chapterRequest.ComicId = comic.ComicId;
            var chapters = _serviceManager.ChapterService.FindChapterComicId(comic.ComicId, chapterRequest);
            Pagination pagination = new Pagination()
            {
                CurrentPage = chapterRequest.PageNumber,
                ItemsPerPage = chapterRequest.PageSize,
                TotalItems = _serviceManager.ChapterService.TotalChapterForComic(chapterRequest)
            };

            return View(new ComicWithChaptersDto()
            {
                Chapters = chapters,
                Comic = comic,
                Pagination = pagination,
            });
        }
    }
}