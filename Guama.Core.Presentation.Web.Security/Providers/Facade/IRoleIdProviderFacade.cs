namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Providers.Facade
{
    public interface IRoleIdProviderFacade
    {
        System.Guid[] GetByPermission(string permission);
    }
}
