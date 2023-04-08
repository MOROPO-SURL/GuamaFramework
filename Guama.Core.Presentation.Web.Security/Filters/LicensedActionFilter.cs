using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Helpers.Facade;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Filters
{
    public class LicensedActionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly ILicenseValidationHelperFacade helper;

        public LicensedActionFilter(ILicenseValidationHelperFacade helper)
        {
            this.helper = helper;
        }

        public void OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            helper.Validate();
        }
    }
}
