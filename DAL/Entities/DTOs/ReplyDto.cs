using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public class ReplyDto
    {
        public int Id { get; set; }
        public string ReplyText { get; set; } = string.Empty;
        public string? UserName { get; set; }
    }
}
