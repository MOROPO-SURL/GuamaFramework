using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes
{
    public class CatchLicenseValidationExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new RedirectToActionResult("index", "licensing", new { Area = "security" });

            base.OnException(context);
        }
    }
}
