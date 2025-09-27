using DAL.Entities;

namespace MangaTor.Models
{
    public class ComicListViewModel
    {
        public IEnumerable<Comic> Comics { get; set; } = Enumerable.Empty<Comic>();
        public Pagination Pagination { get; set; } = new Pagination();
        public int TotalCount => Comics.Count();
    }
}