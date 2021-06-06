using iTechArt.Survey.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTechArt.Survey.Repositories.Configurations
{
    internal class UserConfig: IEntityTypeConfiguration<SurveyUser>
    {
        public void Configure(EntityTypeBuilder<SurveyUser> builder)
        {
        }
    }
}