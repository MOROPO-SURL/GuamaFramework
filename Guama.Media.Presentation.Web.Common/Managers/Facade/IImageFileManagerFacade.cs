namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade
{
    public interface IImageFileManagerFacade
    {
        byte[] GetImageFileContent(Persistence.Entities.Image image);

        string GetImageAbsoluteFilePath(Persistence.Entities.Image image);

        string GetImageRelativeFilePath(Persistence.Entities.Image image);

        void SaveImageFileContent(System.Guid imageId, byte[] content, string contentType);

        void DeleteImageFile(Persistence.Entities.Image image);
    }
}
