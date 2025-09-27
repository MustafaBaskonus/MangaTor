using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Page
    {
        public int ID { get; set; }
        public int PageNumber { get; set; }
        public string ImageUrl { get; set; }
        public Chapter Chapter { get; set; }
        public int ChapterId { get; set; }
    }
}
