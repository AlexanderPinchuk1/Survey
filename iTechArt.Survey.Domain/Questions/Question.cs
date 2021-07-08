using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace iTechArt.Survey.Domain.Questions
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public bool IsRequired { get; set; }

        public List<string> AvailableAnswers { get; set; }

        public QuestionType QuestionType { get; set; }

        public Page Page { get; set; }
    }

    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(question => question.Id);
            builder.Property(question => question.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(question => question.Description).IsRequired();
            builder.Property(question => question.IsRequired).IsRequired();
            builder.Property(question => question.Number).IsRequired();
            builder.Property(question => question.QuestionType).IsRequired();
            builder.Property(question => question.AvailableAnswers)
                .HasConversion(availableAnswers => JsonConvert.SerializeObject(availableAnswers),
                availableAnswers => JsonConvert.DeserializeObject<List<string>>(availableAnswers));
            builder.Property<byte>("QuestionTypeId").IsRequired();
            builder.HasOne(question => question.Page).WithMany(page => page.Questions).IsRequired();
        }
    }
}
