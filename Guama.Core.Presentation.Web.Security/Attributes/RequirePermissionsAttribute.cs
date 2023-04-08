using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Requirements;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Constants;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes
{
    public class RequirePermissionsAttribute : TypeFilterAttribute
    {
        public RequirePermissionsAttribute(params string[] roles) : base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new[] { new RoleAuthorizationRequirement(roles) };
        }

        private class RequiresPermissionAttributeExecutor : System.Attribute, IAsyncResourceFilter
        {
            private readonly RoleAuthorizationRequirement requiredRolePermissions;

            public RequiresPermissionAttributeExecutor(RoleAuthorizationRequirement requiredRolePermissions)
            {
                this.requiredRolePermissions = requiredRolePermissions;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var authorized = context.HttpContext.User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString()));

                if (!authorized)
                {
                    foreach (var requiredRole in requiredRolePermissions.Roles)
                    {
                        if (context.HttpContext.User.IsInRole(requiredRole))
                        {
                            authorized = true;

                            break;
                        }
                    }
                }

                if (!authorized)
                {
                    var authenticationSchema = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.AuthenticationMethod);

                    if (authenticationSchema != null)
                    {
                        context.Result = new ForbidResult(authenticationSchema.Value);
                    }
                    else
                    {
                        context.Result = new ForbidResult();
                    }

                    await context.Result.ExecuteResultAsync(context);
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
