using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "Username is reqired.")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "EMail is reqired.")]
        public string EMail { get; init; }
        [Required(ErrorMessage = "Password is reqired.")]
        public string Password { get; init; }
    }
}