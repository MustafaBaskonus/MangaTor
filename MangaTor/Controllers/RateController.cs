using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.Controllers
{
    [Authorize(Roles ="User,Admin")]
    public class RateController : Controller
    {
        private readonly IServiceManager _services;

        public RateController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(int comicId, int rating)
        {
            await _services.RatingService.AddRateAsync(comicId, rating, this.HttpContext);
            var comic = await _services.ComicService.FindComicWithId(comicId);


            var newScore = (int)_services.RatingService.GetComicAverageScore(comicId);
            return Json(new { success = true, score = newScore });
        }
    }
}
