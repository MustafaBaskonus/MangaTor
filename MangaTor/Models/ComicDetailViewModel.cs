using DAL.Entities;

namespace MangaTor.Models
{
    public class ComicDetailViewModel
    {
        public Comic Comic { get; set; }
        public CommentListViewModel commentWithPaginations{ get; set; } 
        public List<Chapter> Chapters{ get; set; } = new();
        public string NewComment { get; set; }
        public double AverageScore { get; set; } = 0;
        public int? UserScore { get; set; } = 0;

    }
}
