using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.ViewComponents
{
    public class _NavbarDropDawnMenuComponentPartial: ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public _NavbarDropDawnMenuComponentPartial(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var profileImage =  await _serviceManager.ProfileService.GetUserProfileImageAsync(this.HttpContext);
            return View("Default", profileImage);
        }
    }
}
