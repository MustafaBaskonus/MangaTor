using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contacts;

namespace MangaTor.Controllers
{
    [Authorize(Roles = "User")]
    public class ReactionsController : Controller
    {
        private readonly IServiceManager _services;

        public ReactionsController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> AddReaction(int chapterId, int reactionTypeId, string comicSlug)
        {
            await _services.ReactionService.AddReaction(chapterId, reactionTypeId, HttpContext);
            var chapter = await _services.ChapterService.FindChapterAsync(chapterId);

            //return RedirectToAction("Read", "Reader", new { comicSlug = comicSlug, chapterNo = chapter.ChapterNo });
            return RedirectToAction("Read","Reader",new { comicSlug = comicSlug, chapterNo = chapter.ChapterNo },fragment: "reactions");
        }
    }
}
