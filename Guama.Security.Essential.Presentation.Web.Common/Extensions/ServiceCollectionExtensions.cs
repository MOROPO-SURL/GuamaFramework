using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Common.Managers;

namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformSecurityEssentialWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IQueryEntityRepository<Core.Security.Common.Persistence.Entities.Permission, int>, Core.Security.Common.Persistence.ORM.EF.Repositories.PermissionQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Core.Security.Common.Persistence.Entities.Permission, int>, Core.Security.Common.Persistence.ORM.EF.Repositories.PermissionCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Core.Security.Common.Persistence.Entities.Role, System.Guid>, Core.Security.Common.Persistence.ORM.EF.Repositories.RoleQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Core.Security.Common.Persistence.Entities.Role, System.Guid>, Core.Security.Common.Persistence.ORM.EF.Repositories.RoleCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.User, string>, Persistence.ORM.EF.Repositories.UserQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.User, string>, Persistence.ORM.EF.Repositories.UserCommandRepository>();

            services.AddScoped<Core.Presentation.Web.Security.Providers.Facade.IIdentityProviderFacade, IdentityManager>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Core.Security.Common.Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }
    }
}
