using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTOs
{
    public record ResetPasswordDto
    {

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is requıred.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassword is requıred.")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword are must be match.")]
        public string ConfirmPassword { get; set; }

    }

}