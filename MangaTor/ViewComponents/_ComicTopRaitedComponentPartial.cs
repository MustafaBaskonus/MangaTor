using MangaTor.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.ViewComponents
{
    public class _ComicTopRaitedComponentPartial:ViewComponent
    {
        private readonly IServiceManager _services;

        public _ComicTopRaitedComponentPartial(IServiceManager services)
        {
            _services = services;
        }

        public IViewComponentResult Invoke()
        {
            var comics = _services.ComicService.AllComicsComponent(true);
            List<ComicDetailViewModel> dto = new List<ComicDetailViewModel>();
            foreach (var item in comics)
            {
                var averageScore = _services.RatingService.GetComicAverageScore(item.ComicId);
                dto.Add(new ComicDetailViewModel { Comic=item,AverageScore=averageScore});
            }
            var top =dto.OrderByDescending(x => x.AverageScore).Take(3).ToList();
            return View(top);
        }
    }
}
