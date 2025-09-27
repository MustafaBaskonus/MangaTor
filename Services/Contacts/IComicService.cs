using DAL.Entities;
using DAL.Entities.DTOs;
using DAL.Entities.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Contacts
{
    public interface IComicService
    {       
        List<Comic> AllComicsAndChapters(bool TrackChnge, ComicRequestParameters comicRequest);
        Task<Comic> OneComicWithChaptesAsync(string comicSlug);
        Task ComicCreate(AdminComicDto comicDto, IFormFile file);
        Task ComicUpdate(AdminComicDto comicDto, IFormFile file);  
        Task ComicDelete(int id);
        Task<Comic> FindComicWithId(int id);
        Task<Comic> FindComic(string comicSlug);
        
        //paginations
        int TotalComic(ComicRequestParameters comicRequest);



        //rating component
        List<Comic> AllComicsComponent(bool Aktive);
        IEnumerable<Comic> AllComics(bool TrackChnge, ComicRequestParameters comicRequest);


        //List<Comic> ComicsForHomeComponent(bool TrackChnge);
        Task<List<Comic>> ComicsForHomeComponent(bool TrackChnge);



    }
}
