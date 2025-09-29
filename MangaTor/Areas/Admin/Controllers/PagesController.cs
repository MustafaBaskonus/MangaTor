using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PagesController : Controller
    {
        private readonly IServiceManager _services;

        public PagesController(IServiceManager services)
        {
            _services = services;
        }

        // GET: Admin/Pages/Index/{chapterId}
        public async Task<IActionResult> Index(int chapterId)
        {
            var chapter = await _services.ChapterService.FindChapterwithComicAndPagesAsync(chapterId);

            if (chapter == null)
            {
                return NotFound();
            }

            ViewBag.ChapterId = chapterId;
            ViewBag.ComicSlug = chapter.Comic.Slug;
            ViewBag.ChapterNo = chapter.ChapterNo;

            return View(chapter.Pages);
        }

        // GET: Admin/Pages/Create/{chapterId}
        public IActionResult Create(int chapterId)
        {
            ViewBag.ChapterId = chapterId;
            return View();
        }

        // POST: Admin/Pages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int chapterId, List<IFormFile> files)
        {
            var chapter = await _services.ChapterService.FindChapterwithComicAsync(ChapterId: chapterId);

            if (chapter == null || files == null || files.Count == 0)
            {
                return RedirectToAction("Index", new { chapterId });
            }

            await _services.ChapterService.AddImagesForChapter(chapter.ChapterId , files);
            return RedirectToAction("Details","Comics",new {area = "Admin", comicSlug = chapter.Comic.Slug });
            //return RedirectToAction("Index", new { chapterId });
        }
    }
}