using Microsoft.AspNetCore.Mvc;

namespace MangaTor.ViewComponents
{
    public class _ComicFilterComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
