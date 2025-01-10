using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Options;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers
{
    public class RelativePathDocumentFileManager : Facade.IDocumentFileManagerFacade
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private readonly Microsoft.Extensions.Options.IOptions<RelativeMediaFileManagementOptions> options;

        public RelativePathDocumentFileManager(Microsoft.Extensions.Options.IOptions<RelativeMediaFileManagementOptions> options, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
            this.options = options;
        }

        public void DeleteDocumentFile(Persistence.Entities.Document entity)
        {
            System.IO.File.Delete(GetDocumentAbsoluteFilePath(entity));
        }

        public string GetDocumentAbsoluteFilePath(Persistence.Entities.Document entity)
        {
            return System.IO.Path.Combine(env.WebRootPath, options.Value.MediaDirectoryName, options.Value.DocumentsDirectoryName, $"{entity.Id}{MimeTypes.MimeTypeMap.GetExtension(entity.ContentType)}");
        }

        public byte[] GetDocumentFileContent(Persistence.Entities.Document entity)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetDocumentAbsoluteFilePath(entity)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetDocumentRelativeFilePath(Persistence.Entities.Document entity)
        {
            return null;
        }

        public void SaveDocumentFileContent(System.Guid entityId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetDocumentAbsoluteFilePath(new Persistence.Entities.Document { Id = entityId, ContentType = contentType }), content);
        }
    }
}
