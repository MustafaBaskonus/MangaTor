using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.RequestParameters;
using System.Drawing.Printing;
using Services.Extensions;
using DAL.Entities.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class ComicManager : IComicService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ComicManager(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = usermanager;
        }


        public List<Comic> AllComicsAndChapters(bool TrackChnge, ComicRequestParameters comicRequest)
        {
            var comics = _context.Comics
                .Include(m => m.Chapters)
                .OrderByDescending(c => c.ComicId)
                .FilteredComicByCategoryId(comicRequest.CategoryId)
                .SeachComic(comicRequest.SearchTerm)
                .ToPaginate(comicRequest.PageSize, comicRequest.PageNumber)
                .ToList();
            return comics;
        }

        public IEnumerable<Comic> AllComics(bool TrackChnge, ComicRequestParameters comicRequest)
        {
            return _context.Comics
                .OrderByDescending(c => c.ComicId)
                .FilteredComicByCategoryId(comicRequest.CategoryId)
                .SeachComic(comicRequest.SearchTerm)
                .ToPaginate(comicRequest.PageSize, comicRequest.PageNumber);

        }
        public List<Comic> AllComicsComponent(bool Aktive)
        {
            return _context.Comics.Where(m=>m.IsActive==Aktive).ToList();
        }

        public async Task ComicCreate(AdminComicDto comicDto, IFormFile file)
        {
            if (file is not null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "images", "cover", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                comicDto.CoverImageUrl = string.Concat("/images/cover/", file.FileName);
            }
            Comic comic = _mapper.Map<Comic>(comicDto);
            // Seçilen kategorilerle ilişkilendir
            if (comicDto.SelectedCategoryIds != null && comicDto.SelectedCategoryIds.Any())
            {
                comic.Categories = await _context.Categories
                    .Where(c => comicDto.SelectedCategoryIds.Contains(c.CategoryId))
                    .ToListAsync();
            }
            _context.Add(comic);
            await _context.SaveChangesAsync();
        }

        public async Task ComicDelete(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();
        }

        public async Task<Comic> FindComic(string comicSlug)
        {
            var comic = await _context.Comics
                .FirstOrDefaultAsync(c => c.Slug == comicSlug);

            if (comic == null)
            {
                throw new Exception(message: "Comic is not found.");
            }
            return comic;
        }

        public async Task ComicUpdate(AdminComicDto comicDto, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{comicDto.Slug}{extension}";

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", "cover", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                comicDto.CoverImageUrl = string.Concat("/images/cover/", fileName);
            }
            else
            {
                var existing = _context.Comics.AsNoTracking()
                                .FirstOrDefault(x => x.ComicId == comicDto.ComicId);
                comicDto.CoverImageUrl = existing.CoverImageUrl;
            }
            Comic comic = _mapper.Map<Comic>(comicDto);

            // Seçilen kategorilerle ilişkilendir
            if (comicDto.SelectedCategoryIds != null && comicDto.SelectedCategoryIds.Any())
            {
                comic.Categories = await _context.Categories
                    .Where(c => comicDto.SelectedCategoryIds.Contains(c.CategoryId))
                    .ToListAsync();
            }
            _context.Comics.Update(comic);
            _context.SaveChanges();
        }

        public async Task<Comic> FindComicWithId(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic is null)
                throw new Exception(message: "not found comic");
            return comic;
        }

        public Task<Comic> OneComicWithChaptesAsync(string comicSlug)
        {
            return _context.Comics
                .Include(c => c.Chapters.OrderBy(ch => ch.ChapterNo))
                .Include(c => c.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(c => c.Slug == comicSlug);
        }

        public async Task<List<Comic> >ComicsForHomeComponent(bool TrackChnge)
        {
            var comics = await _context.Comics
              .OrderByDescending(c => c.ComicId)
              .Take(8)
              .ToListAsync();

            return comics;
        }

        public int TotalComic(ComicRequestParameters comicRequest)
        {
            var tatolComic = _context.Comics
                .OrderByDescending(c => c.ComicId)
                .FilteredComicByCategoryId(comicRequest.CategoryId)
                .SeachComic(comicRequest.SearchTerm)
                .Count();
            return tatolComic;
        }


    }
}
