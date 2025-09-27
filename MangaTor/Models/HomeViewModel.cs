using DAL.Entities;

namespace MangaTor.Models
{
    public class HomeViewModel
    {
        public List<Comic> LatestComics { get; set; }
        public List<Chapter> LatestChapters { get; set; }
    }
}
