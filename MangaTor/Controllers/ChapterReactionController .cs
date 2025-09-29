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
    using Services.Contacts;

    namespace MangaTor.Controllers
    {
        [Authorize]
        public class ChapterReactionController : Controller
        {
            private readonly IServiceManager _services;

            public ChapterReactionController( IServiceManager services)
            { 
                _services = services;
            }

            // Bölüm için tepki verme formunu göster
            [HttpGet]
            public async Task<IActionResult> ReactForm(int chapterId)
            {
                var chapter = await _services.ChapterService.FindChapterwithComicAsync(chapterId);

                if (chapter == null) return NotFound();

                var reactionTypes = await _services.ReactionService.AllReactType();

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
                if(this.HttpContext.User is null)
                {
                    return Unauthorized();
                }
                await _services.ReactionService.Reacting(chapterId,reactionTypeId,this.HttpContext);
                return RedirectToAction("ReactForm", new { chapterId });
            }
        }
    }
}