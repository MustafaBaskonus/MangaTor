using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaTor.Controllers
{
    using DAL.Context;
    using DAL.Entities;
    using global::MangaTor.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace MangaTor.Controllers
    {
        [Authorize]
        public class ChapterReactionController : Controller
        {
            private readonly AppDbContext _context;
            private readonly UserManager<IdentityUser> _userManager;

            public ChapterReactionController(AppDbContext context, UserManager<IdentityUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            // Bölüm için tepki verme formunu göster
            [HttpGet]
            public async Task<IActionResult> ReactForm(int chapterId)
            {
                var chapter = await _context.Chapters
                    .Include(c => c.Comic)
                    .FirstOrDefaultAsync(c => c.ChapterId == chapterId);

                if (chapter == null) return NotFound();

                var reactionTypes = await _context.ReactionTypes.ToListAsync();

                var model = new ChapterReactionViewModel
                {
                    ChapterId = chapter.ChapterId,
                    ChapterTitle = chapter.Title,
                    ComicTitle = chapter.Comic.Title,
                    ReactionTypes = reactionTypes
                };

                return View(model);
            }

            // Tepkiyi kaydet
            [HttpPost]
            public async Task<IActionResult> React(int chapterId, int reactionTypeId)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Unauthorized();

                var existing = await _context.UserChapterReactions
                    .FirstOrDefaultAsync(r => r.UserId == user.Id && r.ChapterId == chapterId && r.ReactionTypeId == reactionTypeId);

                if (existing != null)
                {
                    _context.UserChapterReactions.Remove(existing);
                }
                else
                {
                    var reaction = new UserChapterReaction
                    {
                        UserId = user.Id,
                        ChapterId = chapterId,
                        ReactionTypeId = reactionTypeId,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.UserChapterReactions.Add(reaction);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("ReactForm", new { chapterId });
            }
        }
    }
}