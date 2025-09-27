using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class ComicServiceExtension
    {
        public static IQueryable<Comic> FilteredComicByCategoryId(this IQueryable<Comic> comics
                , int? categoryId)
        {
            if (categoryId == null)
            {
                return comics;
            }
            else
            {
                return comics.Where(c => c.Categories.Any(cat => cat.CategoryId == categoryId));
            }
        }
        public static IQueryable<Comic> SeachComic(this IQueryable<Comic> comics, string? searchTerm)
        {
            if (searchTerm is null)
            {
                return comics;
            }
            else
            {
                 return comics.Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()));
            }
           

        }
        
        //public static IQueryable<Comic> PriceFilter(this IQueryable<Comic> comics, int? maxPrice, int? minPrice, bool isValid)
        //{
        //    if (!isValid)
        //    {
        //        return comics;
        //    }
        //    return comics.Where(m => m.Price >= minPrice && m.Price <= maxPrice);
        //}
        public static IQueryable<Comic> ToPaginate(this IQueryable<Comic> comics, int pageSize, int pageNumber)
        {
            return comics
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        }
    }
}
