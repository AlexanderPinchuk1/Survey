using System;
using System.Collections.Generic;
using iTechArt.Survey.Domain.Identity;

namespace iTechArt.Survey.Domain.Surveys
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsTemplate { get; set; }

        public SurveyOptions Options { get; set; }

        public User CreatedBy { get; set; }

        public List<Page> Pages { get; set; }
    }
}