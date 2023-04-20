using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Constants;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Requirements;
using System.Threading.Tasks;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes
{
    public class RequirePermissionsAttribute : TypeFilterAttribute
    {
        public RequirePermissionsAttribute(params string[] premissions) : base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new[] { new PermissionAuthorizationRequirement(premissions) };
        }

        private class RequiresPermissionAttributeExecutor : System.Attribute, IAsyncResourceFilter
        {
            private readonly PermissionAuthorizationRequirement requiredRolePermissions;

            public RequiresPermissionAttributeExecutor(PermissionAuthorizationRequirement requiredRolePermissions)
            {
                this.requiredRolePermissions = requiredRolePermissions;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var authorized = context.HttpContext.User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString()));

                var roleProvider = context.HttpContext.RequestServices.GetService<Providers.Facade.IRoleIdProviderFacade>();

                if (!authorized)
                {
                    foreach (var permission in requiredRolePermissions.Permissions)
                    {
                        var roles = roleProvider.GetByPermission(permission);

                        foreach (var role in roles)
                        {
                            if (context.HttpContext.User.IsInRole(role.ToString()))
                            {
                                authorized = true;

                                break;
                            }
                        }

                        if (authorized) break;
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
