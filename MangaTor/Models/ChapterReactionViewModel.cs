using DAL.Entities;

namespace MangaTor.Models
{
    public class ChapterReactionViewModel
    {
        public int ChapterId { get; set; }
        public string ChapterTitle { get; set; }
        public string ComicTitle { get; set; }
        public List<ReactionType> ReactionTypes { get; set; } = new();
    }
}
