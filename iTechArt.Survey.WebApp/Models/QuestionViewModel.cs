using iTechArt.Survey.Domain.Question;
using iTechArt.Survey.Domain.Survey;

namespace iTechArt.Survey.WebApp.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }

        public SurveyTypes SurveyTypes { get; set; }

        public int Number { get; set; }
    }
}
