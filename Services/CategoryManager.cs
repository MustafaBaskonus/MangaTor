
using AutoMapper;
using DAL.Context;
using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryManager(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            bool exists = await _context.Categories.AnyAsync(c => c.Name == category.Name);
            if (exists)
                return false;
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Comics) // ilişkili comic’leri yükle
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category is null)
            {
                throw new Exception("Category not found.");
            }
            // Many-to-Many ilişkide ara tabloyu temizle
            category.Comics.Clear();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryWithTotalChapteDto>> GetAllCategoriesAsync(bool trackChanges = false)
        {
            var categories = await _context.Categories.Include(c => c.Comics).ToListAsync(); 
            var dto =  _mapper.Map<List<CategoryWithTotalChapteDto>>(categories);
            return dto;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(m => m.CategoryId == id);
            if (category is null) { throw new Exception("Category not found."); }
            return category;

        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var existing = await _context.Categories.FindAsync(category.CategoryId);
            if (existing == null)
                return false;

            // Name unique kontrolü
            bool nameExists = await _context.Categories
                .AnyAsync(c => c.Name == category.Name && c.CategoryId != category.CategoryId);
            if (nameExists)
                return false;

            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.IsActive = category.IsActive;
            existing.Slug = category.Slug;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
