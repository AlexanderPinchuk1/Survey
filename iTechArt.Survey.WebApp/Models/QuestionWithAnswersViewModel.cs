using iTechArt.Survey.Domain.Question;
using iTechArt.Survey.Domain.Survey;

namespace iTechArt.Survey.WebApp.Models
{
    public class QuestionWithAnswersViewModel
    {
        public QuestionWithAnswers QuestionWithAnswers{ get; set; }

        public SurveyTypes SurveyTypes { get; set; }

        public int Number { get; set; }
    }
}
