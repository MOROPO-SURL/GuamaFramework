using System.Linq;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static bool IsAuthorized(this System.Security.Claims.ClaimsPrincipal target, string permission)
        {
            if (target.HasClaim(claim => claim.Type.Equals(Core.Presentation.Web.Security.Constants.ClaimTypes.AccountWithFullAccess))) return true;

            var roleIds = target.Claims.Where(item => item.Type == System.Security.Claims.ClaimsIdentity.DefaultRoleClaimType).Select(item => item.Value).ToArray();

            foreach (var roleId in roleIds)
            {
                var permissions = Providers.RoleMemoryProvider.Instance.Roles.First(item => item.Id.ToString() == roleId).PermissionBridges.Select(item => item.Stern.Code).ToArray();

                foreach (var item in permissions)
                {
                    if (item == permission) return true;
                }
            }

            return false;
        }
    }
}
