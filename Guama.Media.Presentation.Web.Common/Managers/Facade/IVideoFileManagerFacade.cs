namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade
{
    public interface IVideoFileManagerFacade
    {
        byte[] GetVideoFileContent(Persistence.Entities.Video video);

        string GetVideoAbsoluteFilePath(Persistence.Entities.Video video);

        string GetVideoRelativeFilePath(Persistence.Entities.Video video);

        void AddVideoFileContentSegment(System.Guid videoId, int segment, byte[] content);

        void AddVideoFileContentSegmentAndClose(System.Guid videoId, byte[] content, string extension);

        void SaveVideoFileContent(System.Guid videoId, byte[] content, string extension);

        void DeleteVideoFileContentSegment(System.Guid videoId, int segment);

        void DeleteVideoFile(System.Guid videoId, string extension);
    }
}
