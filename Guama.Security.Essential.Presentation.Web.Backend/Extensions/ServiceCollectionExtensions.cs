using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Extensions;
using MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Providers;
using MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Common.Extensions;

namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformSecurityEssentialWebBackendServiceRegistration(this IServiceCollection services)
        {
            services.PerformCoreWebSecurityCommonBackendServiceRegistration();
            services.PerformSecurityEssentialWebCommonServiceRegistration();

            services.AddSingleton<Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade, ManagementDashboardSectionProvider>();
        }
    }
}