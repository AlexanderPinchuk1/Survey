using iTechArt.Survey.Configure;
using iTechArt.Survey.Repositories;
using iTechArt.Survey.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iTechArt.Survey
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration config)
        {
            Configuration = config;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));

            services.AddControllersWithViews();

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.AddTransient<IAppVersionService, AppVersionService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
