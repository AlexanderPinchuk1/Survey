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

        public async Task<Pagination> GetEditedPagination(Pagination pagination)
        {
            pagination.ItemCountPerPage = ValidateNumberOfItemsPerPage(pagination.ItemCountPerPage);
            pagination.TotalCount = await GetUsersTotalCountAsync();
            pagination.PageNumber = ValidateNumberOfPages(pagination);

            return pagination;
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

        public async Task<List<UserInfo>> GetUsersInfoForPageAsync(Pagination pagination)
        {
            var users = await _surveyDbContext.Users
                .Skip(pagination.ItemCountPerPage * pagination.PageNumber)
                .Take(pagination.ItemCountPerPage)
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

            return users.Select(user => new UserInfo()
                {
                    Id = user.Id,
                    Role = user.Role,
                    DisplayName = user.DisplayName,
                    RegistrationDateTime = user.RegistrationDateTime,
                    CreatedSurveys = 0,
                    CompletedSurveys = 0,
                }).ToList();
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

        private static int ValidateNumberOfPages(Pagination pagination)
        {
            if (pagination.PageNumber < 0)
            {
                pagination.PageNumber = 0;
            }
            else if (pagination.PageNumber > Math.Ceiling((double)pagination.TotalCount / pagination.ItemCountPerPage) - 1)
            {
                pagination.PageNumber = (int)Math.Ceiling((double)pagination.TotalCount / pagination.ItemCountPerPage) - 1;
            }

            return pagination.PageNumber;
        }
    }
}

