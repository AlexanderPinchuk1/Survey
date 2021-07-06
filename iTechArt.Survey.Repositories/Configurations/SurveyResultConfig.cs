using System;
using iTechArt.Survey.Domain.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class SurveyResultConfig: IEntityTypeConfiguration<SurveyResult>
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
