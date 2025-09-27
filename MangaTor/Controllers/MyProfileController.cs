using AutoMapper;
using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IServiceManager _services;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public MyProfileController(IServiceManager services, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _services = services;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.HttpContext.User);

            var profileDto = _mapper.Map<ProfileDto>(user);
            profileDto.Roles =  new HashSet<string>(await _userManager.GetRolesAsync(user));
            profileDto.comicRating = await _services.RatingService.AllRateForUserId(user.Id);

            return View(profileDto);
        }
    }
}
