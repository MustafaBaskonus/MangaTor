using DAL.Entities;
using DAL.Entities.RequestParameters;
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
    }
}
