using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ReactionType
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Icon { get; set; } // FontAwesome
        public bool IsActive { get; set; } = true;
        public int SequenceNumber { get; set; } = 0;
    }
}
