using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Location.Persistence.Entities;
using MOROPO.Framework.Guama.Location.Presentation.Constants;
using MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.Filter;
using MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View;
using System.Linq;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("location")]
    public class RegionsController : Core.Presentation.Web.Backend.Controllers.Contract.AbstractEntityManagementFilteredController<Region, System.Guid, RegionInputViewModel, RegionDisplayViewModel, RegionFilterModel>
    {
        private readonly IQueryEntityRepository<Country, System.Guid> countryQueryRepository;

        public RegionsController(IQueryEntityRepository<Country, System.Guid> countryQueryRepository, ICommandEntityRepository<Region, System.Guid> commandRepository, IQueryEntityRepository<Region, System.Guid> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer<RegionsController> localizer, Microsoft.Extensions.Logging.ILogger<RegionsController> logger) 
            : base(commandRepository, queryRepository, localizer, logger)
        {
            this.countryQueryRepository = countryQueryRepository;
        }

        [RequirePermissions(Permissions.RegionViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        protected override string GetFilterSessionID()
        {
            return "guamaframework-location-backend-regions-filter";
        }

        protected override System.Func<Region, object> GetOrderSelector()
        {
            return entity => entity.Name;
        }

        [RequirePermissions(Permissions.RegionViewer)]
        public override IActionResult Filter(RegionFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permissions.RegionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permissions.RegionCreator)]
        public override IActionResult Create(RegionInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Permissions.RegionEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permissions.RegionEditor)]
        public override IActionResult Edit(RegionInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Permissions.RegionRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permissions.RegionRemover)]
        public override IActionResult Delete(RegionDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithCountries(RegionInputViewModel modelInput)
        {
            modelInput.Countries = countryQueryRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        protected override void ConfigureCreate(RegionInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(RegionInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(Region entity, RegionInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Region entity, RegionInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void PreCreate(Region entity, RegionInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.CountryId = modelInput.CountryId;
            entity.Country = countryQueryRepository.Get(modelInput.CountryId.Value);
        }

        protected override void PreEdit(Region entity, RegionInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.CountryId != modelInput.CountryId)
            {
                entity.CountryId = modelInput.CountryId;
                entity.Country = countryQueryRepository.Get(modelInput.CountryId.Value);
            }
        }
    }
}