using iTechArt.Survey.Domain;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
