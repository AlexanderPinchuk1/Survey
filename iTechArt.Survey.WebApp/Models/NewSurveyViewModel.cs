using System;
using System.Collections.Generic;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Survey;

namespace iTechArt.Survey.WebApp.Models
{
    public class NewSurveyViewModel
    {
        public List<Page> Pages { get; set; }

        public Guid ActivePage { get; set; }

        public Guid ActiveQuestion { get; set; }

        public SurveyTypes SurveyTypes{ get; set; }
    }
}
