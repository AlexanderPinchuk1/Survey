using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iTechArt.Survey.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace iTechArt.Survey.Foundation
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;


        public UserService(UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<User> FindUserByIdAsyncOrReturnNull(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> EditUserAsync(User user, string role, string displayName)
        {
            if (await _roleManager.RoleExistsAsync(role) && !await _userManager.IsInRoleAsync(user, role))
            {
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, role);
            }

            if (displayName == user.DisplayName)
            {
                return await _userManager.UpdateAsync(user);
            }

            user.DisplayName = displayName;

            await _userManager.ReplaceClaimAsync(user, (await _userManager.GetClaimsAsync(user)).First(claim => claim.Type == "DisplayName"),
                    new Claim("DisplayName", user.DisplayName));

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<string> GetUserRoleAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).First();
        }

        public List<string> GetAllRoles()
        {
            return _roleManager.Roles.Select(role => role.Name).ToList();
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddUserClaimAsync(User user, Claim claim)
        {
            return await _userManager.AddClaimAsync(user, claim);
        }

        public async Task<int> GetUsersTotalCountAsync()
        {
           return await _userManager.Users.CountAsync();
        }

        public async Task<IPagedList<User>> GetUsersPagedListAsync(int numPages, int numItemsPerPage)
        {
           return await _userManager.Users.ToPagedListAsync(numPages, numItemsPerPage);
        }

        public int ValidateNumberOfItemsPerPage(int numItemsPerPage)
        {
            numItemsPerPage = numItemsPerPage switch
            {
                < 1 => 1,
                > 100 => 100,
                _ => numItemsPerPage
            };

            return numItemsPerPage;
        }

        public int ValidateNumberOfPages(int numPages,int numItemsPerPage, int usersTotalCount)
        {
            if (numPages < 1)
            {
                numPages = 1;
            }
            else if (numPages > Math.Ceiling((double)usersTotalCount / numItemsPerPage))
            {
                numPages = (int)Math.Ceiling((double)usersTotalCount / numItemsPerPage);
            }

            return numPages;
        }
    }
}
