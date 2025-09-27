using DAL.Entities;

namespace MangaTor.Models
{
    public class ChapterWithCommentDto
    {
        public Chapter  Chapter{ get; set; }
        public CommentListViewModel commentWithPaginations { get; set; }
    }
}
