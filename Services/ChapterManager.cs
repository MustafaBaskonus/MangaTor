using DAL.Context;
using DAL.Entities;
using DAL.Entities.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChapterManager : IChapterService
    {
        private readonly AppDbContext _context;

        public ChapterManager(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public List<Chapter> FindChapterComicId(int Id, ChapterRequestParameters chapterRequest)
        {
            chapterRequest.PageSize = 3;
            var chapters = _context.Chapters
                .Where(m => m.ComicId == Id)
                .OrderBy(m => m.ChapterNo)
                .Skip((chapterRequest.PageNumber - 1) * chapterRequest.PageSize)
                .Take(chapterRequest.PageSize)
                .ToList();
            return chapters;
        }
        public async Task<Chapter> FindChapterwithComicAsync(int ChapterId)
        {
            var chapter = await _context.Chapters
                    .Include(c => c.Comic)
                    .FirstOrDefaultAsync(c => c.ChapterId == ChapterId);
            return chapter;
        }
        public async Task<Chapter> FindChapterwithComicAndPagesAsync(int chapterNo, string comicSlug)
        {
            var chapter = await _context.Chapters
                                            .Include(c => c.Pages.OrderBy(p => p.PageNumber))
                                            .Include(c => c.Comic)
                                            .FirstOrDefaultAsync(c => c.Comic.Slug == comicSlug && c.ChapterNo == chapterNo);
            return chapter;
        }
        public async Task<Chapter> FindChapterwithComicAndPagesAsync(int chapterId)
        {
            var chapter = await _context.Chapters
                                            .Include(c => c.Pages.OrderBy(p => p.PageNumber))
                                            .Include(c => c.Comic)
                                            .FirstOrDefaultAsync(c => c.ChapterId == chapterId);

            return chapter;
        }
        public async Task<Chapter> FindChapterAsync(int Id)
        {
            var chapter = await _context.Chapters.FindAsync(Id);
            return chapter;
        }

        public IEnumerable<Chapter> HomeChapters(ChapterRequestParameters chapterRequest, int size)
        {
            chapterRequest.PageSize = size;
            var homeChapters = _context.Chapters
                                         .OrderByDescending(c => c.ChapterId)
                                         .Include(c => c.Comic)
                                         .Skip((chapterRequest.PageNumber - 1) * chapterRequest.PageSize)
                                         .Take(chapterRequest.PageSize);
            return homeChapters;
        }

        public int TotalChapter(bool trackchance)
        {
            var totalChapter = _context.Chapters.Count();
            return totalChapter;
        }
        public int TotalChapterForComic(ChapterRequestParameters chapterRequest)
        {
            var totalChapter = _context.Chapters.Where(m => m.ComicId == chapterRequest.ComicId).Count();
            return totalChapter;
        }

        public async Task<List<Chapter>> AllChaptersInSeries(int comicId)
        {
            var allChaptersInSeries = await _context.Chapters
                                                  .Where(c => c.ComicId == comicId)
                                                  .OrderBy(c => c.ChapterNo)
                                                  .ToListAsync();
            return allChaptersInSeries;
        }

        public async Task AddImagesForChapter(int ChapterId, List<IFormFile> files)
        {
            var chapter = await FindChapterwithComicAndPagesAsync(ChapterId);
            // Çizgi romanın resimlerinin saklanacağı yolu oluşturun
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comics", chapter.Comic.Slug, chapter.ChapterNo.ToString());

            // Eğer klasör yoksa oluşturun
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            int pageNumber = _context.Pages.Where(p => p.ChapterId == ChapterId).Count() + 1;

            foreach (var file in files.OrderBy(f => f.FileName))
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Veritabanına sayfa bilgilerini kaydet
                var page = new Page
                {
                    ChapterId = ChapterId,
                    PageNumber = pageNumber,
                    ImageUrl = Path.Combine("/comics", chapter.Comic.Slug, chapter.ChapterNo.ToString(), fileName).Replace("\\", "/")
                };

                _context.Pages.Add(page);
                pageNumber++;
            }

            chapter.TotalPage = files.Count();
            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task<string> DeleteAsync(int chapterId)
        {
            var chapter = await _context.Chapters.FindAsync(chapterId);
            var comic=await _context.Comics.FindAsync(chapter.ComicId);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
            }
            else { throw new NotImplementedException(); }
            return comic.Slug;
        }
        public async Task<string> CreateAsync(Chapter chapter)
        {
            _context.Add(chapter);
            await _context.SaveChangesAsync();

            var comic = await _context.Comics.FindAsync(chapter.ComicId);
            return comic.Slug;
        }


        public async Task<List<Chapter>> AllChapterswithComicAsync()
        {
            return await _context.Chapters
                                         .Include(c => c.Comic)
                                         .ToListAsync();
        }
        public async Task<string> UpdateChapterAsync(Chapter chapter)
        {
            var comic = await _context.Comics.FindAsync(chapter.ComicId);
            _context.Update(chapter);
            await _context.SaveChangesAsync();
            return comic.Slug;
        }
    }
}
