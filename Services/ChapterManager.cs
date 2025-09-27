using DAL.Context;
using DAL.Entities;
using DAL.Entities.RequestParameters;
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
        public async Task<Chapter> FindChapterAsync(int Id)
        {
            var chapter = await _context.Chapters.FindAsync(Id);
            return chapter;
        }

        public IEnumerable<Chapter> HomeChapters(ChapterRequestParameters chapterRequest,int size)
        {
            chapterRequest.PageSize = size;
            var homeChapters=_context.Chapters
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
            var totalChapter = _context.Chapters.Where(m=> m.ComicId== chapterRequest.ComicId).Count();
            return totalChapter;
        }
    }
}
