using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformMiddlewareRegionWebBackendServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade, Providers.ManagementDashboardSectionProvider>();
        }
    }
}
