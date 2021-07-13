using System;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Domain
{
    public class UserAnswer
    {
        public Question Question { get; set; }

        public User User { get; set; }

        public Surveys.Survey Survey { get; set; }

        public string Answer { get; set; }
    }

    public class UserAnswerConfig : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable("UsersAnswer");
            builder.HasOne(usersAnswer => usersAnswer.Survey).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(usersAnswer => usersAnswer.User).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(usersAnswer => usersAnswer.Question).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.Property<Guid>("SurveyId");
            builder.Property<Guid>("UserId");
            builder.Property<Guid>("QuestionId");
            builder.HasKey("SurveyId", "UserId", "QuestionId");
        }
    }
}
