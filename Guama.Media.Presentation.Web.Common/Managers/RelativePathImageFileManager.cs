using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Options;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers
{
    public class RelativePathImageFileManager : Facade.IImageFileManagerFacade
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private readonly Microsoft.Extensions.Options.IOptions<RelativeMediaFileManagementOptions> options;

        public RelativePathImageFileManager(Microsoft.Extensions.Options.IOptions<RelativeMediaFileManagementOptions> options, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
            this.options = options;
        }

        public void DeleteImageFile(Persistence.Entities.Image image)
        {
            System.IO.File.Delete(GetImageAbsoluteFilePath(image));
        }

        public string GetImageAbsoluteFilePath(Persistence.Entities.Image image)
        {
            return System.IO.Path.Combine(env.WebRootPath, options.Value.MediaDirectoryName, options.Value.ImagesDirectoryName, $"{image.Id}{MimeTypes.MimeTypeMap.GetExtension(image.ContentType)}");
        }

        public byte[] GetImageFileContent(Persistence.Entities.Image image)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetImageAbsoluteFilePath(image)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetImageRelativeFilePath(Persistence.Entities.Image image)
        {
            return null;
        }

        public void SaveImageFileContent(System.Guid imageId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetImageAbsoluteFilePath(new Persistence.Entities.Image { Id = imageId, ContentType = contentType }), content);
        }
    }
}
