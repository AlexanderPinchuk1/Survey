using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Survey.WebApp.Models
{
    public class EditUserViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password Hash")]
        public string PasswordHash { get; set; }

        [Required]
        [Display(Name = "Role")]
        [Remote(action: "CheckRole", controller: "Administration", ErrorMessage = "Wrong role")]
        public string Role { get; set; }
    }
}
