using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Domain.Questions;

namespace iTechArt.Survey.Domain
{
    public class UsersAnswer
    {
        public Question Question { get; set; }

        public User User { get; set; }

        public Surveys.Survey Survey{ get; set; }

        public string Answer { get; set; }
    }
}
