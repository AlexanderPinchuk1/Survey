using System;
using System.Collections.Generic;
using iTechArt.Survey.Domain.Questions;

namespace iTechArt.Survey.Domain
{
    public class Page
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public Surveys.Survey Survey { get; set; }

        public List<Question> Questions { get; set; }
    }
}
