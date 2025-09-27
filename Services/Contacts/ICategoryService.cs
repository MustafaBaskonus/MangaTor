using DAL.Entities;
using DAL.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface ICategoryService
    {
        Task<List<CategoryWithTotalChapteDto>> GetAllCategoriesAsync(bool trackChanges);


        Task<bool> CreateCategory(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Category GetCategoryById(int id);
        Task Delete(int id);
    }
}
