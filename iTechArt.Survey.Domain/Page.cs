using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iTechArt.Survey.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Domain
{
    public class Page
    {
        [GuidId]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The page must have a name.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        public Surveys.Survey Survey { get; set; }

        [Required(ErrorMessage = "The page must contain at least one question.")]
        public List<Question> Questions { get; set; }
    }

    public class PageConfig : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Page");
            builder.HasKey(page => page.Id);
            builder.Property(page => page.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(page => page.Name).IsRequired();
            builder.Property(page => page.Number).IsRequired();
            builder.HasMany(page => page.Questions).WithOne(question => question.Page);
        }
    }
}
