using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Providers.Facade;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade
{
    public interface IIdentityManagerFacade : IIdentityProviderFacade
    {
        bool TryModifyPassword(string username, string oldPassword, string newPassword);
    }
}
