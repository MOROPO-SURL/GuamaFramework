using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers
{
    public class RelativePathThumbnailFileManager : Facade.IThumbnailFileManagerFacade
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private readonly Microsoft.Extensions.Options.IOptions<Options.RelativeMediaFileManagementOptions> options;

        public RelativePathThumbnailFileManager(Microsoft.Extensions.Options.IOptions<Options.RelativeMediaFileManagementOptions> options, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
            this.options = options;
        }

        public void DeleteThumbnailFile(Persistence.Entities.Thumbnail thumbnail)
        {
            System.IO.File.Delete(GetThumbnailAbsoluteFilePath(thumbnail));
        }

        public string GetThumbnailAbsoluteFilePath(Persistence.Entities.Thumbnail thumbnail)
        {
            return env.WebRootFileProvider.GetFileInfo(System.IO.Path.Combine(options.Value.MediaDirectoryName, options.Value.ThumbnailsDirectoryName, string.Format("{0}.{1}", thumbnail.Id, thumbnail.ContentType.ContentTypeToExtension()))).PhysicalPath;
        }

        public byte[] GetThumbnailFileContent(Persistence.Entities.Thumbnail thumbnail)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetThumbnailAbsoluteFilePath(thumbnail)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetThumbnailRelativeFilePath(Persistence.Entities.Thumbnail thumbnail)
        {
            return null;
        }

        public void SaveThumbnailFileContent(System.Guid thumbnailId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetThumbnailAbsoluteFilePath(new Persistence.Entities.Thumbnail { Id = thumbnailId, ContentType = contentType }), content);
        }
    }
}
