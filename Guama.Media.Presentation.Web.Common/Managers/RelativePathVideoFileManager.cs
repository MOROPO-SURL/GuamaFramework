using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Options;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers
{
    public class RelativePathVideoFileManager : Facade.IVideoFileManagerFacade
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private readonly Microsoft.Extensions.Options.IOptions<RelativeMediaFileManagementOptions> options;

        public RelativePathVideoFileManager(Microsoft.Extensions.Options.IOptions<RelativeMediaFileManagementOptions> options, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
            this.options = options;
        }

        public void AddVideoFileContentSegment(System.Guid videoId, int segment, byte[] content)
        {
            throw new System.NotImplementedException();
        }

        public void AddVideoFileContentSegmentAndClose(System.Guid videoId, byte[] content, string extension)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteVideoFile(System.Guid videoId, string extension)
        {
            System.IO.File.Delete(GetVideoAbsoluteFilePath(new Persistence.Entities.Video { Id = videoId, Extension = extension }));
        }

        public void DeleteVideoFileContentSegment(System.Guid videoId, int segment)
        {
            throw new System.NotImplementedException();
        }

        public string GetVideoAbsoluteFilePath(Persistence.Entities.Video entity)
        {
            return System.IO.Path.Combine(env.WebRootPath, options.Value.MediaDirectoryName, options.Value.VideosDirectoryName, string.Format("{0}.{1}", entity.Id, entity.Extension));
        }

        public byte[] GetVideoFileContent(Persistence.Entities.Video entity)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetVideoAbsoluteFilePath(entity)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetVideoRelativeFilePath(Persistence.Entities.Video entity)
        {
            throw new System.NotImplementedException();
        }

        public void SaveVideoFileContent(System.Guid entityId, byte[] content, string extension)
        {
            System.IO.File.WriteAllBytes(GetVideoAbsoluteFilePath(new Persistence.Entities.Video { Id = entityId, Extension = extension }), content);
        }
    }
}
