using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public class AdminComicDto
    {
        public int ComicId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }//url için one-pice olacak
        public string? Author { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public ICollection<Chapter>? Chapters { get; set; }
        public bool IsActive { get; set; } = false;
        public List<int> SelectedCategoryIds { get; set; } = new();
    }
}
