using System;
using System.IO;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Foundation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using iTechArt.Survey.Repositories.Extensions;
using Microsoft.Extensions.FileProviders;

namespace iTechArt.Survey.WebApp
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
            services.AddSurveyDbContext(Configuration.GetConnectionString("default"));

            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;
            });

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.AddSingleton<ISurveyCreationService, SurveyCreationService>();
            services.AddTransient<IUserService, UserService>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "node_modules")
                ),
                RequestPath = "/node_modules",
                EnableDirectoryBrowsing = false
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
