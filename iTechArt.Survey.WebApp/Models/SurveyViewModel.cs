using System;
using System.Collections.Generic;
using iTechArt.Survey.Domain.Surveys;

namespace iTechArt.Survey.WebApp.Models
{
    public class SurveyViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public SurveyOptions Options { get; set; }

        public List<PageViewModel> Pages { get; set; }
    }
}
