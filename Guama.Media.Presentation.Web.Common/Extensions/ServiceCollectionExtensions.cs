using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformMiddlewareMediaWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Image, System.Guid>, ImageRepository>();
            services.AddScoped<IEntityRepository<Thumbnail, System.Guid>, ThumbnailRepository>();
            services.AddScoped<IEntityRepository<Video, System.Guid>, VideoRepository>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }

        public static void PerformMiddlewareMediaWebCommonAbsolutePathFileManagementServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Managers.Facade.IImageFileManagerFacade, AbsolutePathImageFileManager>();
            services.AddSingleton<Managers.Facade.IThumbnailFileManagerFacade, AbsolutePathThumbnailFileManager>();
        }

        public static void PerformMiddlewareMediaWebCommonRelativePathFileManagementServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Managers.Facade.IImageFileManagerFacade, RelativePathImageFileManager>();
            services.AddSingleton<Managers.Facade.IThumbnailFileManagerFacade, RelativePathThumbnailFileManager>();
        }
    }
}
