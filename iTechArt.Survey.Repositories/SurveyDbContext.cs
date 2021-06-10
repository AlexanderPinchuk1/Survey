using System;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    internal class SurveyDbContext : IdentityDbContext<User, Role, Guid>
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
        }
    }
}
