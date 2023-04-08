using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Models.View.Contrat;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade
{
    public interface IUserProfileManagerFacade
    {
        IUserProfileViewModel GetUserProfile(string username);

        void PreValidateUserProfile(string username, IUserProfileViewModel profileModel, Microsoft.AspNetCore.Mvc.Controller controller);

        void UpdateUserProfile(string username, IUserProfileViewModel profileModel);
    }
}
