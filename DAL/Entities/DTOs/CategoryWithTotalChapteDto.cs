using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public class CategoryWithTotalChapteDto
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public ICollection<Comic> Comics { get; set; } = new List<Comic>();

        public int TotalComic { get; set; }

    }
}
