using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Kullanıcı ilişkisi
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Comic ilişkisi 
        public int? ComicId { get; set; }
        public Comic? Comic { get; set; }

        // Chapter ilişkisi 
        public int? ChapterId { get; set; }
        public Chapter? Chapter { get; set; }

        // Yorum-Alt yorum ilişkisi
        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }

}
