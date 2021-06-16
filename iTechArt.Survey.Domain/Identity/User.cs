using System;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Survey.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public DateTime RegistrationDateTime { get; set; }
    }
}
