using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iTechArt.Survey.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Domain.Surveys
{
    public class Survey
    {
        [GuidId]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "The survey must have a name.")]
        public string Name { get; set; }

        public bool IsTemplate { get; set; }

        [Range(0, 63)]
        public SurveyOptions Options { get; set; }

        public Guid CreatedById { get; set; }

        public User CreatedBy { get; set; }

        [Required(ErrorMessage = "The survey must contain at least one page.")]
        public List<Page> Pages { get; set; }
    }

    public class SurveyConfig : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
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