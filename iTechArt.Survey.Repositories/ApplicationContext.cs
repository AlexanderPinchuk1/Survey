using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public sealed class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
