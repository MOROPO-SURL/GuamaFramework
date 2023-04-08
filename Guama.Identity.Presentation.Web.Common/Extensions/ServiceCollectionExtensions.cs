using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;
using MOROPO.Framework.Guama.Identity.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using MOROPO.Framework.Guama.Location.Presentation.Web.Common.Extensions;

namespace MOROPO.Framework.Guama.Identity.Presentation.Web.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformMiddlewareIdentityWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.PerformMiddlewareMediaWebCommonServiceRegistration();
            services.PerformMiddlewareRegionWebCommonServiceRegistration();

            services.AddScoped<IEntityRepository<EmailContactItem, System.Guid>, EmailContactItemRepository>();
            services.AddScoped<IEntityRepository<PhoneContactItem, System.Guid>, PhoneContactItemRepository>();
            services.AddScoped<IEntityRepository<SocialContactItem, System.Guid>, SocialContactItemRepository>();
            services.AddScoped<IEntityRepository<WebSiteContactItem, System.Guid>, WebSiteContactItemRepository>();
            services.AddScoped<IEntityRepository<PostalContactItem, System.Guid>, PostalContactItemRepository>();
            services.AddScoped<IEntityRepository<PersonIdentity, System.Guid>, PersonIdentityRepository>();
            services.AddScoped<IEntityRepository<OrganizationIdentity, System.Guid>, OrganizationIdentityRepository>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }
    }
}
