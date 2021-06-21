using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iTechArt.Survey.Domain;

namespace iTechArt.Survey.WebApp.Models
{
    public class DisplayUsersViewModel
    {
        public List<UserInfo> UsersInfo { get; set; }

        [Display(Name = "Items count per page")]
        public int ItemCountPerPage { get; set; }

        public int PageNumber { get; set; }

        public int TotalCount { get; set; }
    }
}
