using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public class ProfileDto
    {
        public string UserName { get; init; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
        public bool EmailConfirmed { get; init; }
        public string? ProfileImageUrl { get; set; }
        public HashSet<String> Roles { get; set; } = new HashSet<string>();
        public List< ComicRating>? comicRating { get; set; }
    }
}
