using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PagesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PagesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Pages/Index/{chapterId}
        // Bir bölüme ait sayfaları listeler.
        public async Task<IActionResult> Index(int chapterId)
        {
            var chapter = await _context.Chapters
                                        .Include(c => c.Comic)
                                        .Include(c => c.Pages.OrderBy(p => p.PageNumber))
                                        .FirstOrDefaultAsync(c => c.ChapterId == chapterId);

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
            var chapter = await _context.Chapters
                                .Include(c => c.Comic) // Bu satır çok önemli!
                                .FirstOrDefaultAsync(c => c.ChapterId == chapterId);

            if (chapter == null || files == null || files.Count == 0)
            {
                return RedirectToAction("Index", new { chapterId });
            }

            // Çizgi romanın resimlerinin saklanacağı yolu oluşturun
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "comics", chapter.Comic.Slug, chapter.ChapterNo.ToString());

            // Eğer klasör yoksa oluşturun
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            int pageNumber = _context.Pages.Where(p => p.ChapterId == chapterId).Count() + 1;

            foreach (var file in files.OrderBy(f => f.FileName))
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Veritabanına sayfa bilgilerini kaydet
                var page = new Page
                {
                    ChapterId = chapterId,
                    PageNumber = pageNumber,
                    ImageUrl = Path.Combine("/comics", chapter.Comic.Slug, chapter.ChapterNo.ToString(), fileName).Replace("\\", "/")
                };
                
                _context.Pages.Add(page);
                pageNumber++;
            }

            chapter.TotalPage = files.Count();
            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Comics", new { area = "Admin", comicSlug = chapter.Comic.Slug });
            //return RedirectToAction("Index", new { chapterId });
        }
    }
}