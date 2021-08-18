using System;

namespace iTechArt.Survey.Domain.Surveys
{
    public class SurveyInfo
    {
        public Guid Id { get; set; }

        public string Name{ get; set; }

        public DateTime CreationDateTime{ get; set; }

        public int AnswersCount { get; set; }

        public string Link { get; set; }
    }
}
