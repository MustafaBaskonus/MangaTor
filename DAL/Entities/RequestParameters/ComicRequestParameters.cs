using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.RequestParameters
{
    public class ComicRequestParameters : RequestParameters
    {


        public  int? CategoryId { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public ComicRequestParameters():this(4,1)
        {

        }
        public ComicRequestParameters(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
