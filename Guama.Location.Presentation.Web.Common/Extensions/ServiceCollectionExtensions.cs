using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformLocationWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.Country, System.Guid>, CountryQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.Country, System.Guid>, CountryCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.State, System.Guid>, StateQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.State, System.Guid>, StateCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.Municipality, System.Guid>, MunicipalityQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.Municipality, System.Guid>, MunicipalityCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.Locality, System.Guid>, LocalityQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.Locality, System.Guid>, LocalityCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.Region, System.Guid>, RegionQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.Region, System.Guid>, RegionCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.Location, System.Guid>, LocationQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.Location, System.Guid>, LocationCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.StateLocation, System.Guid>, StateLocationQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.StateLocation, System.Guid>, StateLocationCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.MunicipalityLocation, System.Guid>, MunicipalityLocationQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.MunicipalityLocation, System.Guid>, MunicipalityLocationCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Persistence.Entities.LocalityLocation, System.Guid>, LocalityLocationQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Persistence.Entities.LocalityLocation, System.Guid>, LocalityLocationCommandRepository>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }
    }
}
