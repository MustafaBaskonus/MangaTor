using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.ViewComponents
{
    public class _CategoriesMenuComponentPartial : ViewComponent
    {
        private readonly IServiceManager _services;

        public _CategoriesMenuComponentPartial(IServiceManager services)
        {
            _services = services;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesDto = await _services.CategoryService.GetAllCategoriesAsync(false);
            return View(categoriesDto);
        }
    }
}
