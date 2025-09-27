using AutoMapper;
using DAL.Context;
using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReactionManager : IReactionService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ReactionManager(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        //read page get ractions
        public async Task<List<ReactionTypeDto>> GetReactionTypesDto(int id)
        {
            var reactions = await _context.ReactionTypes.Where(m => m.IsActive == true).OrderBy(m => m.SequenceNumber).ToListAsync();
            var reactionTypeDto = new List<ReactionTypeDto>();

            var chapterReactions = _context.UserChapterReactions.Where(m => m.ChapterId == id);
            foreach (var item in reactions)
            {
                var dto = _mapper.Map<ReactionTypeDto>(item);
                dto.ReactionCount = chapterReactions.Where(m => m.ReactionTypeId == dto.Id).Count();
                reactionTypeDto.Add(dto);
            }

            return reactionTypeDto;
        }


        //read page AddReaction
        public async Task AddReaction(int chapterId, int reactionTypeId, HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);

            // Kullanıcının daha önce aynı chapter için aynı tepkiyi verip vermediğini kontrol et
            var existing = _context.UserChapterReactions
                .FirstOrDefault(r => r.ChapterId == chapterId &&
                                     r.ReactionTypeId == reactionTypeId &&
                                     r.UserId == user.Id);

            if (existing == null)
            {
                var newReaction = new UserChapterReaction
                {
                    UserId = user.Id,
                    ChapterId = chapterId,
                    ReactionTypeId = reactionTypeId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.UserChapterReactions.Add(newReaction);
            }
            else
            {
                _context.UserChapterReactions.Remove(existing);
            }
            await _context.SaveChangesAsync();
        }
    }
}
