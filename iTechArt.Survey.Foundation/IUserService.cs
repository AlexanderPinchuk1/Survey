using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Survey.Foundation
{
    public interface IUserService
    {
        public Task<IdentityResult> EditUserAsync(User user, string role, string displayName);

        public Task<IdentityResult> DeleteUserAsync(User user);
        
        public Task<IdentityResult> AddUserAsync(User user, string password);

        public Task<IdentityResult> AddUserRoleAndClaimAsync(User user, string role);

        public Task<PagedEntities<UserInfo>> GetUsersInfoForPageAsync(int pageIndex, int itemCountPerPage);

        public Task<User> FindUserByIdAsyncOrReturnNull(Guid id);

        public Task<string> GetUserRoleAsync(User user);

        public Task<bool> IsEmailUniqueAsync(Guid userId, string email);
    }
}
