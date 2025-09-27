using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public record class ReactionTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; } // FontAwesome
        public bool IsActive { get; set; } = true;
        public int SequenceNumber { get; set; } = 0;
        public int ReactionCount { get; set; } = 0;
    }
}
