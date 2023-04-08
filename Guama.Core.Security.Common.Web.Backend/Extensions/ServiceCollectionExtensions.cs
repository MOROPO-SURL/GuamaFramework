using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformCoreWebSecurityCommonBackendServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper, Helpers.DataInitializationHelper>();
        }
    }
}
