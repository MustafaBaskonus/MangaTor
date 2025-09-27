using DAL.Entities;
using DAL.Entities.DTOs;

namespace MangaTor.Models
{
    public class CommentListViewModel
    {
        public IEnumerable<CommentDtoForDetail> Comments { get; set; } = Enumerable.Empty<CommentDtoForDetail>();
        public Pagination Pagination { get; set; } = new Pagination();
        public int TotalCount => Comments.Count();
    }
}
 