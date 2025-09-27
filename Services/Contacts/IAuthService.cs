using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> GetRoles();
        //Index
        Task<IEnumerable<UserDtoForUpdate>> GetUsersDtoAsymc();
        Task<IdentityUser> GetOneUserAsync(string Name);
        Task<UserDtoForUpdate> GetOneUserForUpdateAsync(string Name);

        Task<IdentityResult> CreateUserAsync(UserDtoForInsertion userDtoForInsertion);
        Task UpdateUserAsync(UserDtoForUpdate dtoForUpdate);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task DeleteAsync(string UserName);

    }

}