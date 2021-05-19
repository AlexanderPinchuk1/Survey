using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public sealed class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
