using System;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Domain.Questions;
using iTechArt.Survey.Domain.Surveys;
using iTechArt.Survey.Repositories.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public class SurveyDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Domain.Surveys.Survey> Surveys { get; set; }

        public DbSet<SurveyResult> SurveyResults { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionTypeLookup> QuestionTypeLookups { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }


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
