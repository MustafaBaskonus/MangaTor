using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.ViewComponents
{
    public class _ReactionsComponentPartial : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _ReactionsComponentPartial(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task< IViewComponentResult >InvokeAsync(int chapterId)
        {
            var reactionsDto = await _manager.ReactionService.GetReactionTypesDto(chapterId);
            ViewBag.ChapterId = chapterId;
            return View(reactionsDto);
        }
    }
}
