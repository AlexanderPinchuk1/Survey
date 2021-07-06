using iTechArt.Survey.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class QuestionConfig: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(question => question.Id);
            builder.Property(question => question.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(question => question.Description).IsRequired();
            builder.Property(question => question.IsRequired).IsRequired();
            builder.Property(question => question.Missed).IsRequired();
            builder.Property(question => question.Number).IsRequired();
            builder.Property<byte>("QuestionTypeId").IsRequired();
            builder.HasOne(question => question.QuestionType);
            builder.HasOne(question => question.Page).WithMany(page => page.Questions).IsRequired();
        }
    }
}
