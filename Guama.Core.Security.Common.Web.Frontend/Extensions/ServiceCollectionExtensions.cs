using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.Framework.Guama.Core.Security.Common.Web.Frontend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformCoreWebSecurityCommonFrontendServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Presentation.Web.Security.Filters.LicensedActionFilter>();
            services.AddSingleton<Presentation.Web.Security.Filters.ChangeUserPasswordRequiredActionFilter>();
        }
    }
}
