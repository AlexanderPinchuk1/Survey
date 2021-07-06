using iTechArt.Survey.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class PageConfig: IEntityTypeConfiguration<Page>
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
