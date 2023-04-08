using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Constants;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Filters
{
    public class ChangeUserPasswordRequiredActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (context.HttpContext.User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountChangePasswordRequired) && claim.Value.Equals(true.ToString())))
                {
                    var notification = new Presentation.Models.View.NotificationViewModel
                    {
                        Type = Presentation.Models.View.NotificationType.Warning,
                        Message = "PasswordChangeRequiredMessage"
                    };

                    (context.Controller as Controller).TempData[Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

                    throw new Exceptions.ChangeUserPasswordRequiredException();
                }
            }
        }
    }
}
