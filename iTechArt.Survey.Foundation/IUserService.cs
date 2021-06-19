using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using iTechArt.Survey.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using X.PagedList;

namespace iTechArt.Survey.Foundation
{
    public interface IUserService
    {
        public Task<IdentityResult> EditUserAsync(User user, string role, string displayName);

        public Task<IdentityResult> DeleteUserAsync(User user);
        
        public Task<IdentityResult> AddUserAsync(User user, string password);

        public Task<IdentityResult> AddUserClaimAsync(User user, Claim claim);

        public Task<IdentityResult> AddUserToRoleAsync(User user, string role);

        public Task<IPagedList<User>> GetUsersPagedListAsync(int numPages, int numItemsPerPage);

        public Task<User> FindUserByIdAsyncOrReturnNull(string id);

        public Task<int> GetUsersTotalCountAsync();

        public Task<string> GetUserRoleAsync(User user);

        public List<string> GetAllRoles();

        public int ValidateNumberOfItemsPerPage(int numItemsPerPage);

        public int ValidateNumberOfPages(int numPages, int numItemsPerPage, int usersTotalCount);
    }
}
