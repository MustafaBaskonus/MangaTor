using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.Controllers
{
    [Authorize(Roles ="User,Admin")]
    public class RateController : Controller
    {
        private readonly IServiceManager _service;

        public RateController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(int comicId, int rating)//, HttpContext httpContext
        {
            await _service.RatingService.AddRateAsync(comicId, rating, this.HttpContext);
            var comic = await _service.ComicService.FindComicWithId(comicId);


            var newScore = (int)_service.RatingService.GetComicAverageScore(comicId);
            return Json(new { success = true, score = newScore });
        }
    }
}
