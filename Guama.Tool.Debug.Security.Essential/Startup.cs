using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Extensions;
using MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Extensions;
using Serilog;
using System.Linq;

namespace MOROPO.Framework.Guama.Tool.Debug.Security.Essential
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Persistence.EF.ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, Persistence.EF.ApplicationDbContext>();

            services.PerformSecurityEssentialWebBackendServiceRegistration();

            services.AddHttpContextAccessor();

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(config =>
            {
                config.AccessDeniedPath = new PathString("/security/error/forbidden");
                config.LoginPath = new PathString("/security/account/login");
                config.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
                config.SlidingExpiration = true;
            });

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc(mvc =>
            {
                mvc.EnableEndpointRouting = false;
                mvc.ModelBinderProviders.Insert(0, new Core.Presentation.Web.Common.ModelBinding.AbstractsModelBinderProvider());
            });

            services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(env.ContentRootPath, "dpkeys")));

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> requestLocalizationOptions, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, DbContext dbContext)
        {
            loggerFactory.AddSerilog(Log.Logger);

            app.UseDeveloperExceptionPage();

            dbContext.Database.Migrate();

            var dataInitializationHelpers = app.ApplicationServices.GetServices<Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper>();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var scopedDbContext = serviceScope.ServiceProvider.GetService<DbContext>();

                foreach (var dataInitializationHelper in dataInitializationHelpers)
                {
                    dataInitializationHelper.InitialiseData(scopedDbContext);
                }
            }

            if (!(dbContext as Persistence.EF.ApplicationDbContext).Users.Any())
            {
                var userId = configuration["DefaultUser:Id"];

                var defaultUser = new Guama.Security.Essential.Persistence.Entities.User
                {
                    Id = userId,
                    Password = configuration["DefaultUser:Password"].ToHashedPassword(),
                    Enabled = true,
                    HasFullAccess = true,
                    PasswordChangeRequired = true
                };

                defaultUser.Signature = Guama.Security.Essential.Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(defaultUser);

                dbContext.Set<Guama.Security.Essential.Persistence.Entities.User>().Add(defaultUser);

                (dbContext as Core.Persistence.ORM.EF.Context.Contract.AbstractTraceableDbContext).SaveChanges("system");
            }

            app.UseExceptionHandler("/error");

            app.UseHsts();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaultLocalizedAreaRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultLocalizedRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute("defaultAreaRoutes",
                    template: "{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultRoutes",
                    template: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
