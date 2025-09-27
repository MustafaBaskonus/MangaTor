using AutoMapper;
using DAL.Entities;
using DAL.Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(UserDtoForInsertion userDtoForInsertion)
        {
            var user = _mapper.Map<ApplicationUser>(userDtoForInsertion);
            var result = await _userManager.CreateAsync(user, userDtoForInsertion.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User could not be created.");
            }
            if (userDtoForInsertion.Roles.Count > 0)
            {
                var roleresult = await _userManager.AddToRolesAsync(user, userDtoForInsertion.Roles);
                if (!roleresult.Succeeded)
                {
                    throw new Exception("System have problems with roles.");
                }

            }
            return result;
        }

        public async Task DeleteAsync(string UserName)
        {
            var user = await GetOneUserAsync(UserName);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> GetOneUserAsync(string Name)
        {
            var user = await _userManager.FindByNameAsync(Name);
            if (user is not null)
            {
                return user;
            }
            throw new Exception("User is not found.");
        }
        
        public async Task<UserDtoForUpdate> GetOneUserForUpdateAsync(string Name)
        {
            var user = await GetOneUserAsync(Name);
            if (user is not null)
            {
                var userDto = _mapper.Map<UserDtoForUpdate>(user);
                userDto.Roles = new HashSet<string>(GetRoles().Select(x => x.Name));
                userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
                return userDto;
            }
            throw new Exception("User is not found");

        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles;
        }

        public async Task< IEnumerable<UserDtoForUpdate>> GetUsersDtoAsymc()
        {
            var users = _userManager.Users.ToList();

            var userDtos = new List<UserDtoForUpdate>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<UserDtoForUpdate>(user);

                // Kullanıcının kendi rolleri
                dto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));

                userDtos.Add(dto);
            }

            return userDtos;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByNameAsync(resetPasswordDto.UserName);
            if (user is not null)
            {
                await _userManager.RemovePasswordAsync(user);
                var result = await _userManager.AddPasswordAsync(user, resetPasswordDto.Password);
                return result;
            }
            throw new Exception("User not found.");
        }

        public async Task UpdateUserAsync(UserDtoForUpdate dtoForUpdate)
        {

            var user = await GetOneUserAsync(dtoForUpdate.UserName);
            user.Email = dtoForUpdate.EMail;
            user.PhoneNumber = dtoForUpdate.PhoneNumber;
            if (user is not null)
            {
                await _userManager.UpdateAsync(user);
                if (dtoForUpdate.Roles.Count > 0)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var r1 = await _userManager.RemoveFromRolesAsync(user, roles);
                    var r2 = await _userManager.AddToRolesAsync(user, dtoForUpdate.Roles);
                }
                return;
            }
            throw new Exception("User not update.");

        }
    }

}