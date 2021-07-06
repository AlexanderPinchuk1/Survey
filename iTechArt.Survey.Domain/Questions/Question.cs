using System;

namespace iTechArt.Survey.Domain.Questions
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public bool IsRequired { get; set; }

        public int Missed { get; set; }

        public string AvailableAnswers { get; set; }

        public QuestionType QuestionType { get; set; }

        public Page Page { get; set; }
    }
}
