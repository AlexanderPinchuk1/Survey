using System;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Domain
{
    public class UserAnswer
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public Guid? UserId { get; set; }

        public User User { get; set; }

        public Guid SurveyId { get; set; }

        public Surveys.Survey Survey { get; set; }

        public string Answer { get; set; }
    }

    public class UserAnswerConfig : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable("UsersAnswer");
            builder.HasOne(userAnswer => userAnswer.Survey).WithMany()
                .HasForeignKey(userAnswer => userAnswer.SurveyId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(userAnswer => userAnswer.User).WithMany()
                .HasForeignKey(userAnswer => userAnswer.UserId)
                .IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(userAnswer => userAnswer.Question).WithMany()
                .HasForeignKey(userAnswer => userAnswer.QuestionId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasKey(userAnswer => new { userAnswer.Id , userAnswer.SurveyId, userAnswer.QuestionId});
            builder.Property(userAnswer => userAnswer.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
