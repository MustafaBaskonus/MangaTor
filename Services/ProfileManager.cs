using AutoMapper;
using DAL.Context;
using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProfileManager: IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProfileManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProfileDto> GetUserProfileAsync(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);

            var profileDto = _mapper.Map<ProfileDto>(user);
            profileDto.Roles = new HashSet<string>(await _userManager.GetRolesAsync(user));
            profileDto.comicRating = _context.ComicRatings.Where(m=> m.UserId==user.Id).Include(m=>m.Comic).ToList();
            return profileDto;
        }

        public async Task<string> GetUserProfileImageAsync(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user.ProfileImageUrl is null)
                return "/images/profile/Default/pic-1.png";
            return user.ProfileImageUrl;
        }
        
        public async Task UpdateProfileImage(HttpContext httpContext, IFormFile file)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user is null)
            {
                throw new Exception(message: "User  not found.");
            }
            else if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{user.UserName}{extension}";

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", "profile", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                user.ProfileImageUrl = string.Concat("/images/profile/", fileName);
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
