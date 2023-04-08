namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Controllers
{
    public class LicensingController : Common.Controllers.BaseWebController
    {
        public LicensingController(Microsoft.Extensions.Localization.IStringLocalizer<LicensingController> localizer, Microsoft.Extensions.Logging.ILogger<LicensingController> logger)
            : base(localizer, logger)
        {
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return View();
        }
    }
}
