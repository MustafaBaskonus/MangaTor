using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReactionsController : Controller
    {
        private readonly AppDbContext _context;

        public ReactionsController(AppDbContext context)
        {
            _context = context;
        }

        // ReactionType listeleme
        public async Task<IActionResult> Index()
        {
            var reactions = await _context.ReactionTypes.ToListAsync();
            return View(reactions);
        }

        // ReactionType ekleme formu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ReactionType ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(ReactionType reactionType)
        {
            if (!ModelState.IsValid)
                return View(reactionType);

            _context.ReactionTypes.Add(reactionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ReactionType düzenleme formu
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reaction = await _context.ReactionTypes.FindAsync(id);
            if (reaction == null) return NotFound();
            return View(reaction);
        }

        // ReactionType güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Edit(ReactionType reactionType)
        {
            if (!ModelState.IsValid)
                return View(reactionType);

            _context.ReactionTypes.Update(reactionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ReactionType silme formu
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var reaction = await _context.ReactionTypes.FindAsync(id);
            if (reaction == null) return NotFound();
            return View(reaction);
        }

        // ReactionType silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaction = await _context.ReactionTypes.FindAsync(id);
            if (reaction != null)
            {
                _context.ReactionTypes.Remove(reaction);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }












        // Chapter reaksiyonlarını listeleme (UserChapterReaction)
        public async Task<IActionResult> ChapterReactions()
        {
            var reactions = await _context.UserChapterReactions
                .Include(r => r.User)
                .Include(r => r.Chapter)
                .Include(r => r.ReactionType)
                .ToListAsync();

            return View(reactions);
        }

        // Chapter reaksiyonunu silme
        [HttpPost]
        public async Task<IActionResult> DeleteChapterReaction(int id)
        {
            var reaction = await _context.UserChapterReactions.FindAsync(id);
            if (reaction != null)
            {
                _context.UserChapterReactions.Remove(reaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ChapterReactions));
        }
    }
}
