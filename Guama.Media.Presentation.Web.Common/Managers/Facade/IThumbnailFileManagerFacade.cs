namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade
{
    public interface IThumbnailFileManagerFacade
    {
        void SaveThumbnailFileContent(System.Guid thumbnailId, byte[] content, string contentType);

        byte[] GetThumbnailFileContent(Persistence.Entities.Thumbnail thumbnail);

        string GetThumbnailAbsoluteFilePath(Persistence.Entities.Thumbnail thumbnail);

        string GetThumbnailRelativeFilePath(Persistence.Entities.Thumbnail thumbnail);

        void DeleteThumbnailFile(Persistence.Entities.Thumbnail thumbnail);
    }
}
