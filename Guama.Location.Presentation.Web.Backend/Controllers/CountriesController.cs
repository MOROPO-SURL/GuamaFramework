using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.Filter;
using MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Location.Persistence.Entities;
using MOROPO.Framework.Guama.Location.Presentation.Constants;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("location")]
    public class CountriesController : Core.Presentation.Web.Backend.Controllers.Contract.AbstractEntityManagementFilteredController<Country, System.Guid, CountryInputViewModel, CountryDisplayViewModel, CountryFilterModel>
    {
        public CountriesController(IEntityRepository<Country, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CountriesController> localizer, Microsoft.Extensions.Logging.ILogger<CountriesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permissions.CountryViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        protected override string GetFilterSessionID()
        {
            return "guamaframework-location-backend-countries-filter";
        }

        protected override System.Func<Country, object> GetOrderSelector()
        {
            return entity => entity.Name;
        }

        [RequirePermissions(Permissions.CountryViewer)]
        public override IActionResult Filter(CountryFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permissions.CountryCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permissions.CountryCreator)]
        public override IActionResult Create(CountryInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Permissions.CountryEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permissions.CountryEditor)]
        public override IActionResult Edit(CountryInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Permissions.CountryRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permissions.CountryRemover)]
        public override IActionResult Delete(CountryDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
