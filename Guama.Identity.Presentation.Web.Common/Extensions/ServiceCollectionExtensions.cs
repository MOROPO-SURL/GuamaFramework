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
        public static void PerformIdentityWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.PerformMediaWebCommonServiceRegistration();
            services.PerformLocationWebCommonServiceRegistration();

            services.AddScoped<IQueryEntityRepository<EmailContactItem, System.Guid>, EmailContactItemQueryRepository>();
            services.AddScoped<ICommandEntityRepository<EmailContactItem, System.Guid>, EmailContactItemCommandRepository>();
            services.AddScoped<IQueryEntityRepository<PhoneContactItem, System.Guid>, PhoneContactItemQueryRepository>();
            services.AddScoped<ICommandEntityRepository<PhoneContactItem, System.Guid>, PhoneContactItemCommandRepository>();
            services.AddScoped<IQueryEntityRepository<SocialContactItem, System.Guid>, SocialContactItemQueryRepository>();
            services.AddScoped<ICommandEntityRepository<SocialContactItem, System.Guid>, SocialContactItemCommandRepository>();
            services.AddScoped<IQueryEntityRepository<WebSiteContactItem, System.Guid>, WebSiteContactItemQueryRepository>();
            services.AddScoped<ICommandEntityRepository<WebSiteContactItem, System.Guid>, WebSiteContactItemCommandRepository>();
            services.AddScoped<IQueryEntityRepository<PostalContactItem, System.Guid>, PostalContactItemQueryRepository>();
            services.AddScoped<ICommandEntityRepository<PostalContactItem, System.Guid>, PostalContactItemCommandRepository>();
            services.AddScoped<IQueryEntityRepository<PersonIdentity, System.Guid>, PersonIdentityQueryRepository>();
            services.AddScoped<ICommandEntityRepository<PersonIdentity, System.Guid>, PersonIdentityCommandRepository>();
            services.AddScoped<IQueryEntityRepository<OrganizationIdentity, System.Guid>, OrganizationIdentityQueryRepository>();
            services.AddScoped<ICommandEntityRepository<OrganizationIdentity, System.Guid>, OrganizationIdentityCommandRepository>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }
    }
}
