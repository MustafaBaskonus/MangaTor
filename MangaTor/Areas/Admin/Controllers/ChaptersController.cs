using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System.Linq;
using System.Threading.Tasks;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChaptersController : Controller
    {
        private readonly IServiceManager _services;

        public ChaptersController( IServiceManager services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            var chapters = _services.ChapterService.AllChapterswithComicAsync();
            return View(chapters);
        }


        public IActionResult Create(int? comicId)
        {
            if (comicId == null)
            {
                return BadRequest("Bölüm eklemek için bir çizgi roman serisi seçmelisiniz.");
            }

            ViewBag.ComicId = comicId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Chapter chapter)
        {
            var slug = await _services.ChapterService.CreateAsync(chapter);

            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = slug });

        }

        // GET: Admin/Chapters/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var chapter = _services.ChapterService.FindChapterwithComicAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            return View(chapter);
        }

        // POST: Admin/Chapters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Chapter chapter)
        {
            if (id != chapter.ChapterId)
            {
                return NotFound();
            }

            var slug = await _services.ChapterService.UpdateChapterAsync(chapter);

            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = slug });

        }


        public async Task<IActionResult> Delete(int id)
        {
            var slug = await _services.ChapterService.DeleteAsync(id);
            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = slug });
        }


    }
}