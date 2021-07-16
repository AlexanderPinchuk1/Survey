using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Foundation
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SurveyDbContext _surveyDbContext;

        public UserService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SurveyDbContext surveyDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _surveyDbContext = surveyDbContext;
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

        public async Task<bool> IsEmailUniqueAsync(Guid userId, string email)
        {
            return !await _userManager.Users.Where(user => user.Id != userId && user.Email == email).AnyAsync();
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

        private async Task<int> GetUsersTotalCountAsync()
        {
           return await _userManager.Users.CountAsync();
        }

        public async Task<PagedEntities<UserInfo>> GetUsersInfoForPageAsync(int pageIndex, int itemCountPerPage)
        {
            itemCountPerPage = ValidateNumberOfItemsPerPage(itemCountPerPage);
            var totalCount = await GetUsersTotalCountAsync();
            pageIndex = ValidateNumberOfPages(pageIndex,  itemCountPerPage,  totalCount);

            var users = await _surveyDbContext.Users
                .Skip(itemCountPerPage * pageIndex)
                .Take(itemCountPerPage)
                .Join(_surveyDbContext.UserRoles, user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new
                    {
                        user.Id,
                        user.DisplayName,
                        user.RegistrationDateTime,
                        userRole.RoleId
                    })
                .Join(_surveyDbContext.Roles, user => user.RoleId, role => role.Id,
                    (user, role) => new
                    {
                        user.Id,
                        user.DisplayName,
                        user.RegistrationDateTime,
                        Role = role.Name
                    }).ToListAsync();
            
            return new PagedEntities<UserInfo>(pageIndex, itemCountPerPage, totalCount, users.Select(user => new UserInfo()
            {
                Id = user.Id,
                Role = user.Role,
                DisplayName = user.DisplayName,
                RegistrationDateTime = user.RegistrationDateTime,
                CreatedSurveys = 0,
                CompletedSurveys = 0,
            }).ToList());
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

        private static int ValidateNumberOfPages(int pageIndex, int itemCountPerPage, int totalCount)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            else if (pageIndex > Math.Ceiling((double)totalCount / itemCountPerPage) - 1)
            {
                pageIndex = (int)Math.Ceiling((double)totalCount / itemCountPerPage) - 1;
            }

            return pageIndex;
        }
    }
}

