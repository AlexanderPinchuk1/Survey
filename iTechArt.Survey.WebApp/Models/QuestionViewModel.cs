using iTechArt.Survey.Domain.Question;

namespace iTechArt.Survey.WebApp.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }

        public bool IsQuestionHaveNumber { get; set; }

        public int Number { get; set; }
    }
}
