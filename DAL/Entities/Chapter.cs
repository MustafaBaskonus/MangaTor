using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public  class Chapter
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public int ChapterNo { get; set; }
        public Comic Comic { get; set; }
        public int ComicId {  get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public ICollection<Page>? Pages { get; set; }
        public bool IsActive { get; set; }=false;
        public int? TotalPage { get; set; } = 0;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<UserChapterReaction> Reactions { get; set; } = new List<UserChapterReaction>();
    }
}
