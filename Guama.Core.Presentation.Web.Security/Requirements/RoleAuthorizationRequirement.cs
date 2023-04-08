namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Requirements
{
    public class RoleAuthorizationRequirement : Microsoft.AspNetCore.Authorization.IAuthorizationRequirement
    {
        public System.Collections.Generic.IEnumerable<string> Roles { get; }

        public RoleAuthorizationRequirement(System.Collections.Generic.IEnumerable<string> roles)
        {
            Roles = roles;
        }
    }
}
