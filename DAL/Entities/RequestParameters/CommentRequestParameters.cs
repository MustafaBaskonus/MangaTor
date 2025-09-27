using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.RequestParameters
{
    public class CommentRequestParameters : RequestParameters
    {
        public  int? ComicId { get; set; }
        public  int? ChapterId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public CommentRequestParameters():this(5,1)
        {
        }
        public CommentRequestParameters(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
