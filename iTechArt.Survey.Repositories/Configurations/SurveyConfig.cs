using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class SurveyConfig: IEntityTypeConfiguration<Domain.Surveys.Survey>
    {
        public void Configure(EntityTypeBuilder<Domain.Surveys.Survey> builder)
        {
            builder.ToTable("Survey");
            builder.HasKey(survey => survey.Id);
            builder.Property(survey => survey.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(survey => survey.IsTemplate).IsRequired();
            builder.Property(survey => survey.Name).IsRequired().HasMaxLength(50);
            builder.Property(survey => survey.Options).IsRequired();
            builder.HasOne(survey => survey.CreatedBy);
            builder.HasMany(survey => survey.Pages).WithOne(page => page.Survey).IsRequired();
        }
    }
}
