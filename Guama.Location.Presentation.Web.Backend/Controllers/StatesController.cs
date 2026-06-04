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
    public class StatesController : Core.Presentation.Web.Backend.Controllers.Contract.AbstractEntityManagementFilteredController<State, System.Guid, StateInputViewModel, StateDisplayViewModel, StateFilterModel>
    {
        private readonly IQueryEntityRepository<Region, System.Guid> regionQueryRepository;

        public StatesController(IQueryEntityRepository<Region, System.Guid> regionQueryRepository, ICommandEntityRepository<State, System.Guid> commandRepository, IQueryEntityRepository<State, System.Guid> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer<StatesController> localizer, Microsoft.Extensions.Logging.ILogger<StatesController> logger) 
            : base(commandRepository, queryRepository, localizer, logger)
        {
            this.regionQueryRepository = regionQueryRepository;
        }

        [RequirePermissions(Permissions.StateViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        protected override string GetFilterSessionID()
        {
            return "guamaframework-location-backend-states-filter";
        }

        protected override System.Func<State, object> GetOrderSelector()
        {
            return entity => entity.Name;
        }

        [RequirePermissions(Permissions.StateViewer)]
        public override IActionResult Filter(StateFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permissions.StateCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permissions.StateCreator)]
        public override IActionResult Create(StateInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Permissions.StateEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permissions.StateEditor)]
        public override IActionResult Edit(StateInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Permissions.StateRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permissions.StateRemover)]
        public override IActionResult Delete(StateDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithCountries(StateInputViewModel modelInput)
        {
            modelInput.Regions = regionQueryRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        protected override void ConfigureCreate(StateInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(StateInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(State entity, StateInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(State entity, StateInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void PreCreate(State entity, StateInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.RegionId = modelInput.RegionId;
            entity.Region = regionQueryRepository.Get(modelInput.RegionId.Value);
        }

        protected override void PreEdit(State entity, StateInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.RegionId != modelInput.RegionId)
            {
                entity.RegionId = modelInput.RegionId;
                entity.Region = regionQueryRepository.Get(modelInput.RegionId.Value);
            }
        }
    }
}
