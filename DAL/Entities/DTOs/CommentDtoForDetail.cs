using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public class CommentDtoForDetail
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Kullanıcı ilişkisi
        public string UserId { get; set; }
        public string UserName { get; set; }

        // Comic ilişkisi 
        public int? ComicId { get; set; }
        public string? ComicTitle { get; set; }

        // Chapter ilişkisi 
        public int? ChapterId { get; set; }
        public string? ChapterTitle { get; set; }

        // Yorum-Alt yorum ilişkisi
        public int? ParentCommentId { get; set; }
        public string? CommentUserName { get; set; }
        public List<CommentDtoForDetail> Replies { get; set; } = new();
    }
}
