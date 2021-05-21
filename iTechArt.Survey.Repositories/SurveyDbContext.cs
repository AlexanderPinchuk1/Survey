using iTechArt.Survey.Domain;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    internal sealed class SurveyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(SurveyDbContext).Assembly);
        }
    }
}
