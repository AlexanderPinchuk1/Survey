using System;
using iTechArt.Survey.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Domain.Surveys
{
    public class SurveyResult
    {
        public User User { get; set; }

        public Survey Survey { get; set; }

        public DateTime CompletionDate { get; set; }
    }

    public class SurveyResultConfig : IEntityTypeConfiguration<SurveyResult>
    {
        public void Configure(EntityTypeBuilder<SurveyResult> builder)
        {
            builder.ToTable("SurveyResult");
            builder.HasOne(surveyResult => surveyResult.Survey);
            builder.HasOne(surveyResult => surveyResult.User);
            builder.Property(surveyResult => surveyResult.CompletionDate).IsRequired();
            builder.Property<Guid>("UserId");
            builder.Property<Guid>("SurveyId");
            builder.HasKey("UserId", "SurveyId");
        }
    }
}
