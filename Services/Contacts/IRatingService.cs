using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IRatingService
    {
        Task AddRateAsync(int ComicId, int Score, HttpContext httpContext);
        //comic details
        double GetComicAverageScore(int ComicId);
        //comic details
        Task<int> GetScoreByUser(int ComicId, HttpContext httpContext);


    }
}
