using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformMiddlewareRegionWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Persistence.Entities.Country, System.Guid>, CountryRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.State, System.Guid>, StateRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.Municipality, System.Guid>, MunicipalityRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.Locality, System.Guid>, LocalityRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.Region, System.Guid>, RegionRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.Location, System.Guid>, LocationRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.StateLocation, System.Guid>, StateLocationRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.MunicipalityLocation, System.Guid>, MunicipalityLocationRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.LocalityLocation, System.Guid>, LocalityLocationRepository>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }
    }
}
