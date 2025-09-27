using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserChapterReaction
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }

        public int ReactionTypeId { get; set; }
        public ReactionType ReactionType { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
