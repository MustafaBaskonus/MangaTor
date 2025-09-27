using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceManager _services;

        public CategoryController(IServiceManager services)
        {
            _services = services;
        }

        public async Task<IActionResult >Index()
        {
            var categoriesDto = await _services.CategoryService
                .GetAllCategoriesAsync(false);
            return View(categoriesDto);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult > Create(Category category)
        {
            if (category is null)
            {
                return NotFound();
            }
            var result = await _services.CategoryService.CreateCategory(category);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View();           
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category =  _services.CategoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            if (id != model.CategoryId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _services.CategoryService.UpdateCategoryAsync(model);

            if (result) // result bool
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kategori güncellenemedi.");
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
                return NotFound();

            var category = _services.CategoryService.GetCategoryById(id);
                

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.CategoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
