using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IProfileService
    {
        Task<ProfileDto> GetUserProfileAsync(HttpContext httpContext);
        Task<string> GetUserProfileImageAsync(HttpContext httpContext);

        Task UpdateProfileImage(HttpContext httpContext, IFormFile file);
    }
}
