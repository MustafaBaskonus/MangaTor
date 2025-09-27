using DAL.Entities;

namespace MangaTor.Models
{
    public class ChapterListViewModel
    {
        public IEnumerable<Chapter> Chapters { get; set; } = Enumerable.Empty<Chapter>();
        public Pagination Pagination { get; set; } = new Pagination();
        public int TotalCount => Chapters.Count();
    }
}