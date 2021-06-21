using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


        public async Task<User> FindUserByIdAsyncOrReturnNull(Guid id)
        {
            return await _userManager.FindByIdAsync(Convert.ToString(id));
        }

        public async Task<IdentityResult> EditUserAsync(User user, string role, string displayName)
        {
            if (await _roleManager.RoleExistsAsync(role) && !await _userManager.IsInRoleAsync(user, role))
            {
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, role);
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

        public async Task<List<string>> GetAllRoles()
        {
            return await _roleManager.Roles.Select(role => role.Name).ToListAsync();
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserRoleAndClaimAsync(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
            
            return await _userManager.AddClaimAsync(user, new Claim("DisplayName", user.DisplayName));
        }

        public async Task<int> GetUsersTotalCountAsync()
        {
           return await _userManager.Users.CountAsync();
        }

        public async Task<List<UserInfo>> GetUsersInfoForPageAsync(int pageNumber, int itemsCountPerPage)
        {
            itemsCountPerPage = ValidateNumberOfItemsPerPage(itemsCountPerPage);
            var usersTotalCount = await GetUsersTotalCountAsync();
            pageNumber = ValidateNumberOfPages(pageNumber, itemsCountPerPage, usersTotalCount);

            var users = await _userManager.Users.Skip(itemsCountPerPage * pageNumber).Take(itemsCountPerPage).ToListAsync();

            var usersInfo = new List<UserInfo>();
            foreach (var user in users)
            {
                var role = await GetUserRoleAsync(user);

                usersInfo.Add(new UserInfo()
                {
                    Id = user.Id,
                    Role = role,
                    DisplayName = user.DisplayName,
                    RegistrationDateTime = user.RegistrationDateTime,
                    CreatedSurveys = 0,
                    CompletedSurveys = 0,
                });
            }

            return usersInfo;
        }

        private static int ValidateNumberOfItemsPerPage(int numItemsPerPage)
        {
            numItemsPerPage = numItemsPerPage switch
            {
                < 1 => 1,
                > 100 => 100,
                _ => numItemsPerPage
            };

            return numItemsPerPage;
        }

        private static int ValidateNumberOfPages(int numPages,int numItemsPerPage, int usersTotalCount)
        {
            if (numPages < 0)
            {
                numPages = 0;
            }
            else if (numPages > Math.Ceiling((double)usersTotalCount / numItemsPerPage))
            {
                numPages = (int)Math.Ceiling((double)usersTotalCount / numItemsPerPage);
            }

            return numPages;
        }
    }
}
