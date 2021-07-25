using System;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Repositories.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public class SurveyDbContext : IdentityDbContext<User, Role, Guid>
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(SurveyDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(Domain.Surveys.Survey).Assembly);

            modelBuilder.Seed();
        }
    }
}
