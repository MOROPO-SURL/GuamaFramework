using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Exceptions;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes
{
    public class CatchChangeUserPasswordRequiredExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ChangeUserPasswordRequiredException)
            {
                context.Result = new RedirectToActionResult("password", "account", new { Area = "security" });

                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
