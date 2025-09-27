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
    }
}
