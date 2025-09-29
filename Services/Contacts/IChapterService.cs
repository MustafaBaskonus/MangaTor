using DAL.Entities;
using DAL.Entities.RequestParameters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IChapterService
    {
        IEnumerable<Chapter> HomeChapters(ChapterRequestParameters chapteRequest, int size);
        int TotalChapter(bool trackchance);
        int TotalChapterForComic(ChapterRequestParameters chapterRequest);

        List<Chapter> FindChapterComicId(int Id , ChapterRequestParameters chapteRequest);
        //reaction contoller
        Task<Chapter> FindChapterAsync(int Id);

        //chapterReaction controller
        Task<Chapter> FindChapterwithComicAsync(int ChapterId);
        //ReaderController
        Task<Chapter> FindChapterwithComicAndPagesAsync(int chapterNo, string comicSlug);
        //ReaderController
        Task<List<Chapter>> AllChaptersInSeries(int comicId);
        //Admin Pages controller
        Task<Chapter> FindChapterwithComicAndPagesAsync(int chapterId);
        //Admin Pages controller
        Task AddImagesForChapter(int ChapterId, List<IFormFile> files);
        //Admin Chapter controller
        Task<string> DeleteAsync(int chapterId);
        //Admin Chapter Controller
        Task<string> CreateAsync(Chapter chapter);
        //Admin Chapter Controller
        Task<List<Chapter>> AllChapterswithComicAsync();
        //Admin Chapter Controller
        Task<string> UpdateChapterAsync(Chapter chapter);

    }
}
