using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Common.Extensions;

namespace MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformSecurityPersonalWebBackendServiceRegistration(this IServiceCollection services)
        {
            services.PerformCoreWebSecurityCommonBackendServiceRegistration();
            services.PerformSecurityPersonalWebCommonServiceRegistration();

            services.AddSingleton<Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade, Providers.ManagementDashboardSectionProvider>();
        }
    }
}