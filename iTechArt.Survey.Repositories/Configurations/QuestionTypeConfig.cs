using iTechArt.Survey.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class QuestionTypeConfig: IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder.ToTable("QuestionType");
            builder.HasKey(questionType => questionType.Id);
            builder.Property(questionType => questionType.Id).ValueGeneratedOnAdd();
            builder.Property(questionType => questionType.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(questionType => questionType.Name).IsUnique();
        }
    }
}
