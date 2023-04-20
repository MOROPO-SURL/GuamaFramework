namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Requirements
{
    public class PermissionAuthorizationRequirement : Microsoft.AspNetCore.Authorization.IAuthorizationRequirement
    {
        public System.Collections.Generic.IEnumerable<string> Permissions { get; }

        public PermissionAuthorizationRequirement(System.Collections.Generic.IEnumerable<string> permissions)
        {
            Permissions = permissions;
        }
    }
}
