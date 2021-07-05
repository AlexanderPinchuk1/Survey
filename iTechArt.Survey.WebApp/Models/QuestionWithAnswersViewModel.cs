using iTechArt.Survey.Domain.Question;

namespace iTechArt.Survey.WebApp.Models
{
    public class QuestionWithAnswersViewModel
    {
        public QuestionWithAnswers QuestionWithAnswers{ get; set; }

        public bool IsQuestionHaveNumber { get; set; }

        public int Number { get; set; }
    }
}
