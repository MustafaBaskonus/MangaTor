using DAL.Context;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {

        private readonly IComicService _comicService;
        private readonly IChapterService _chapterService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;
        private readonly ICommentService _commentService;
        private readonly IReactionService _reactionService;
        private readonly IRatingService _ratingService;
        private readonly IProfileService _profileService;

        public ServiceManager(IComicService comicService, IChapterService chapterService, ICategoryService categoryService,
            IAuthService authService, ICommentService commentService, IReactionService reactionService, IRatingService ratingService, IProfileService profileService)
        {
            _comicService = comicService;
            _chapterService = chapterService;
            _categoryService = categoryService;
            _authService = authService;
            _commentService = commentService;
            _reactionService = reactionService;
            _ratingService = ratingService;
            _profileService = profileService;
        }

        public IComicService ComicService => _comicService;

        public IChapterService ChapterService => _chapterService;
        public ICategoryService CategoryService => _categoryService;

        public IAuthService AuthService => _authService;
        public ICommentService CommentService => _commentService;
        public IReactionService ReactionService => _reactionService;
        public IRatingService RatingService => _ratingService;

        public IProfileService ProfileService => _profileService;
    }
}
