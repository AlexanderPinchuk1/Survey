using iTechArt.Survey.Domain.Questions;
using iTechArt.Survey.Domain.Surveys;

namespace iTechArt.Survey.WebApp.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }

        public SurveyOptions SurveyOptions { get; set; }

        public string Answer { get; set; }
    }
}
