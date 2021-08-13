using iTechArt.Repositories.UnitOfWork;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Foundation;
using iTechArt.Survey.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using iTechArt.Survey.Repositories.Extensions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;
            });

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.AddTransient<IUserService, UserService>();

            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddScoped<IUnitOfWork, SurveyUnitOfWork>();
            services.AddScoped<ISurveyUnitOfWork, SurveyUnitOfWork>();
            services.AddScoped<ISurveyService, SurveyService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
