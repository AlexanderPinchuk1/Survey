using iTechArt.SurveyCreator.Models;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.SurveyCreator.Repositories
{
    public sealed class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
          

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
