namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers
{
    public class AbsolutePathThumbnailFileManager : Facade.IThumbnailFileManagerFacade
    {
        private readonly Microsoft.Extensions.Options.IOptions<Options.AbsoluteMediaFileManagementOptions> options;

        public AbsolutePathThumbnailFileManager(Microsoft.Extensions.Options.IOptions<Options.AbsoluteMediaFileManagementOptions> options)
        {
            this.options = options;
        }

        public void DeleteThumbnailFile(Persistence.Entities.Thumbnail thumbnail)
        {
            System.IO.File.Delete(GetThumbnailAbsoluteFilePath(thumbnail));
        }

        public string GetThumbnailAbsoluteFilePath(Persistence.Entities.Thumbnail thumbnail)
        {
            return System.IO.Path.Combine(options.Value.ThumbnailsDirectoryPath, $"{thumbnail.Id}{MimeTypes.MimeTypeMap.GetExtension(thumbnail.ContentType)}");
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
