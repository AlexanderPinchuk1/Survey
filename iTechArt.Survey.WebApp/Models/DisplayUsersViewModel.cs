using System.Collections.Generic;
using iTechArt.Survey.Domain;

namespace iTechArt.Survey.WebApp.Models
{
    public class DisplayUsersViewModel
    {
        public List<UserInfo> UsersInfo { get; set; }

        public Pagination Pagination { get; set; } = new();
    }
}
