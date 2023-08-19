namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers
{
    public class AbsolutePathImageFileManager : Facade.IImageFileManagerFacade
    {
        private readonly Microsoft.Extensions.Options.IOptions<Options.AbsoluteMediaFileManagementOptions> options;

        public AbsolutePathImageFileManager(Microsoft.Extensions.Options.IOptions<Options.AbsoluteMediaFileManagementOptions> options)
        {
            this.options = options;
        }

        public void DeleteImageFile(Persistence.Entities.Image image)
        {
            System.IO.File.Delete(GetImageAbsoluteFilePath(image));
        }

        public string GetImageAbsoluteFilePath(Persistence.Entities.Image image)
        {
            return System.IO.Path.Combine(options.Value.ImagesDirectoryPath, string.Format("{0}.{1}", image.Id, MimeTypes.MimeTypeMap.GetExtension(image.ContentType)));
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
