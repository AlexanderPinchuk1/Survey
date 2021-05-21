using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace iTechArt.Survey.Repositories.Extensions
{
    public static class SurveyDbContextService
    {
        public static IServiceCollection AddSurveyDbContext(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<SurveyDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
