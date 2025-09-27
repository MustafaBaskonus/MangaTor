using DAL.Entities;

namespace MangaTor.Models
{
    public class ComicWithChaptersDto
    {
        public List<Chapter> Chapters { get; set; }
        public Comic Comic { get; set; }
        public Pagination Pagination { get; set; }
    }
}
