using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChaptersController : Controller
    {
        private readonly AppDbContext _context;

        public ChaptersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var chapters = await _context.Chapters
                                         .Include(c => c.Comic)
                                         .ToListAsync();
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

            _context.Add(chapter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = _context.Comics.Find(chapter.ComicId).Slug });

        }

        // GET: Admin/Chapters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter =  _context.Chapters.Include(m=> m.Comic).FirstOrDefault(m=>m.ChapterId==id);
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


            try
            {
                _context.Update(chapter);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(chapter.ChapterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = _context.Comics.Find(chapter.ComicId).Slug });

        }


        public async Task<IActionResult> Delete(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = _context.Comics.Find(chapter.ComicId).Slug });
        }

        private bool ChapterExists(int id)
        {
            return _context.Chapters.Any(e => e.ChapterId == id);
        }
    }
}