﻿using System;
using System.ComponentModel.DataAnnotations;

namespace iTechArt.Survey.Domain
{
    public class UserInfo
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public DateTime RegistrationDateTime { get; set; }

        [Required]
        public int CompletedSurveys { get; set; }

        [Required]
        public int CreatedSurveys { get; set; }
    }
}
