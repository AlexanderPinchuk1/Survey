using System;
using System.Collections.Generic;
using iTechArt.Survey.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

    public class PageConfig: IEntityTypeConfiguration<Page>
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
