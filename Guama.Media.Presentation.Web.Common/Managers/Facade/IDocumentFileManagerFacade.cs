namespace MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade
{
    public interface IDocumentFileManagerFacade
    {
        byte[] GetDocumentFileContent(Persistence.Entities.Document entity);

        string GetDocumentAbsoluteFilePath(Persistence.Entities.Document entity);

        string GetDocumentRelativeFilePath(Persistence.Entities.Document entity);

        void SaveDocumentFileContent(System.Guid entityId, byte[] content, string contentType);

        void DeleteDocumentFile(Persistence.Entities.Document entity);
    }
}
