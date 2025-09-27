using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ComicRating
    {
        public int Id { get; set; }
        public int ComicId { get; set; }
        public Comic Comic { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int Score { get; set; } // 1–5 arası
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
