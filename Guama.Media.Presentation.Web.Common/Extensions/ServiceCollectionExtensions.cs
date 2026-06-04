using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void PerformMediaWebCommonServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IQueryEntityRepository<Image, System.Guid>, ImageQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Image, System.Guid>, ImageCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Thumbnail, System.Guid>, ThumbnailQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Thumbnail, System.Guid>, ThumbnailCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Video, System.Guid>, VideoQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Video, System.Guid>, VideoCommandRepository>();
            services.AddScoped<IQueryEntityRepository<Document, System.Guid>, DocumentQueryRepository>();
            services.AddScoped<ICommandEntityRepository<Document, System.Guid>, DocumentCommandRepository>();

            services.AddSingleton<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.DbSchemaConfigurationProvider>();
        }

        public static void PerformMediaWebCommonAbsolutePathFileManagementServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Managers.Facade.IImageFileManagerFacade, AbsolutePathImageFileManager>();
            services.AddSingleton<Managers.Facade.IThumbnailFileManagerFacade, AbsolutePathThumbnailFileManager>();
        }

        public static void PerformMediaWebCommonRelativePathFileManagementServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<Managers.Facade.IImageFileManagerFacade, RelativePathImageFileManager>();
            services.AddSingleton<Managers.Facade.IThumbnailFileManagerFacade, RelativePathThumbnailFileManager>();
            services.AddSingleton<Managers.Facade.IVideoFileManagerFacade, RelativePathVideoFileManager>();
            services.AddSingleton<Managers.Facade.IDocumentFileManagerFacade, RelativePathDocumentFileManager>();
        }
    }
}
