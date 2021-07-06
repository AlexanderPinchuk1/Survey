using System;
using iTechArt.Survey.Domain.Identity;

namespace iTechArt.Survey.Domain.Surveys
{
    public class SurveyResult
    {
        public User User { get; set; }

        public Survey Survey { get; set; }

        public DateTime CompletionDate { get; set; }
    }
}
