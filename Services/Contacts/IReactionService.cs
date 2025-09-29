using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IReactionService
    {
        Task<List<ReactionTypeDto>> GetReactionTypesDto(int id);
        Task AddReaction(int chapterId, int reactionTypeId,HttpContext httpContext);
        //ChapterReacting controller
        Task<List<ReactionType>> AllReactType();
        //ChapterReaction  controller
        Task Reacting(int chapterId, int reactionTypeId, HttpContext httpContext);

        //Admin controller
        Task<bool> CreateReactionTypeAsync(ReactionType reactionType);
        //admin reaction controller
        Task<ReactionType> FindReactionTypeAsync(int id);

        Task UpdateReactionTypeAsync(ReactionType reactionType);
        Task Delete(ReactionType reactionType);

    }
}
