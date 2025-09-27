using DAL.Entities.RequestParameters;
using MangaTor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;

namespace MangaTor.ViewComponents
{
    public class _HomeLastComicComponentPartial:ViewComponent
    {
        private readonly IServiceManager _manager;

        public _HomeLastComicComponentPartial(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult > InvokeAsync()
        {
            var LatestComics =await _manager.ComicService
                                .ComicsForHomeComponent(false);

            return View(LatestComics);
        }

    }
}
