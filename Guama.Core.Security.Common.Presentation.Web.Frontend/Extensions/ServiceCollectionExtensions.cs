using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformCoreWebSecurityCommonFrontendServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Core.Presentation.Web.Security.Filters.LicensedActionFilter>();
            services.AddSingleton<Core.Presentation.Web.Security.Filters.ChangeUserPasswordRequiredActionFilter>();
            services.AddSingleton<IRoleMemoryManagerFacade, Managers.RoleMemoryManager>();
        }
    }
}
