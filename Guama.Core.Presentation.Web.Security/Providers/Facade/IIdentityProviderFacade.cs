namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Providers.Facade
{
    public interface IIdentityProviderFacade
    {
        System.Security.Claims.ClaimsIdentity ProvideIdentity(string username, string password, string authenticationType);

        int GetCaptchaCodeLength();
    }
}