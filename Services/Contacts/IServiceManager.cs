using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IServiceManager
    {
        IComicService ComicService { get; }
        IChapterService ChapterService { get; }
        ICategoryService CategoryService { get; }
        IAuthService AuthService { get; }
        ICommentService CommentService { get; }
        IReactionService ReactionService { get; }
        IRatingService RatingService { get; }

    }
}
