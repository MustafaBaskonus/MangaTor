using MangaTor.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.ViewComponents
{
    public class _HomeSliderSectionComponentPartial: ViewComponent
    {
        private readonly IServiceManager _services;

        public _HomeSliderSectionComponentPartial(IServiceManager services)
        {
            _services = services;
        }
        //geçici gelecek duyurular olabilir
        public IViewComponentResult Invoke()
        {
            var comics = _services.ComicService.AllComicsComponent(true);
            List<ComicDetailViewModel> dto = new List<ComicDetailViewModel>();
            foreach (var item in comics)
            {
                var averageScore = _services.RatingService.GetComicAverageScore(item.ComicId);
                dto.Add(new ComicDetailViewModel { Comic = item, AverageScore = averageScore });
            }
            var top = dto.OrderByDescending(x => x.AverageScore).Take(6).ToList();
            return View(top);
        }
    }
}
