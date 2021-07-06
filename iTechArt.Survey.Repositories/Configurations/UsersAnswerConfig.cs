using System;
using iTechArt.Survey.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class UsersAnswerConfig: IEntityTypeConfiguration<UsersAnswer>
    {
        public void Configure(EntityTypeBuilder<UsersAnswer> builder)
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
