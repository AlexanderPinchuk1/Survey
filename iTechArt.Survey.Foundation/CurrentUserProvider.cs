using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace iTechArt.Survey.Foundation
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public Guid GetUserId()
        {
            var stringGuid = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            return Guid.TryParse(stringGuid, out var guid) ? guid : Guid.Empty;
        }
    }
}