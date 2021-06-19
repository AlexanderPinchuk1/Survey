using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArt.Survey.WebApp.Models
{
    public class DisplayUsersViewModel
    {
        public List<UserInfoViewModel> UsersInfo { get; set; }

        [Display(Name = "Num items per page")]
        public int NumItemsPerPage { get; set; }

        public int NumPage { get; set; }

        public int TotalCount { get; set; }

        public DisplayUsersViewModel()
        {
            NumPage = 1;
            NumItemsPerPage = 5;
        }
    }
}
