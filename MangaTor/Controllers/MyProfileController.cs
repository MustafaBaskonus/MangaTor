using AutoMapper;
using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;

namespace MangaTor.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class MyProfileController : Controller
    {
        private readonly IServiceManager _services;

        public MyProfileController(IServiceManager services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            var profileDto = await _services.ProfileService.GetUserProfileAsync(this.HttpContext);
            return View("Index",profileDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileImage()
        {
            var profileImageUrl = await _services.ProfileService.GetUserProfileImageAsync(this.HttpContext);
            var defaultImages = new List<string>()
            {
                profileImageUrl,
                "/images/profile/Default/pic-1.png",
                "/images/profile/Default/pic-2.png",
                "/images/profile/Default/pic-3.png",
                "/images/profile/Default/pic-4.png"
            };
            return View("ProfileImage", profileImageUrl);
        }
        [HttpPost]
        public async Task<IActionResult> ProfileImage(IFormFile file)
        {
            await _services.ProfileService.UpdateProfileImage(this.HttpContext, file);
            return RedirectToAction("Index");
        }
    }
}
