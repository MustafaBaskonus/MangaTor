using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingManager : IRatingService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingManager(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task AddRateAsync(int ComicId, int Score, HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);

            var existing = _context.ComicRatings.FirstOrDefault(m => m.UserId == user.Id && m.ComicId == ComicId);
            if (existing is null)
            {
                var rate = new ComicRating { ComicId = ComicId, UserId = user.Id, Score = Score };
                _context.ComicRatings.Add(rate);
            }
            else
            {
                existing.Score = Score;
                existing.CreatedAt = DateTime.Now;
            }
            _context.SaveChanges(); 
        }

        public double GetComicAverageScore(int ComicId)
        {
            var scores = _context.ComicRatings
                                 .Where(m => m.ComicId == ComicId)
                                 .Select(m => m.Score);
            double averageScore = 0;
            if (!scores.Any()) return averageScore; // Hiç puan yoksa 0 döndür

            averageScore = scores.Average();
            return averageScore;
        }

        public async Task<int> GetScoreByUser(int ComicId, HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null) return 0; // kullanıcı giriş yapmamışsa 0

            var rating = _context.ComicRatings
                                 .FirstOrDefault(m => m.UserId == user.Id && m.ComicId == ComicId);

            return rating?.Score ?? 0; // rating yoksa 0 döndür
        }
    }
}
