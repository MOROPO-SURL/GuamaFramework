using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class MunicipalitiesController : Core.Presentation.Web.Backend.Controllers.Contract.AbstractEntityManagementFilteredController<Municipality, System.Guid, MunicipalityInputViewModel, MunicipalityDisplayViewModel, MunicipalityFilterModel>
    {
        private readonly IEntityRepository<State, System.Guid> stateRepository;

        public MunicipalitiesController(IEntityRepository<State, System.Guid> stateRepository, IEntityRepository<Municipality, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<MunicipalitiesController> localizer, Microsoft.Extensions.Logging.ILogger<MunicipalitiesController> logger) : base(repository, localizer, logger)
        {
            this.stateRepository = stateRepository;
        }

        [RequirePermissions(Permissions.MunicipalityViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        protected override string GetFilterSessionID()
        {
            return "guamaframework-location-backend-municipalities-filter";
        }

        protected override System.Func<Municipality, object> GetOrderSelector()
        {
            return entity => entity.Name;
        }

        [RequirePermissions(Permissions.MunicipalityViewer)]
        public override IActionResult Filter(MunicipalityFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permissions.MunicipalityCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }


        [RequirePermissions(Permissions.MunicipalityCreator)]
        public override IActionResult Create(MunicipalityInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Permissions.MunicipalityEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permissions.MunicipalityEditor)]
        public override IActionResult Edit(MunicipalityInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Permissions.MunicipalityRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permissions.MunicipalityRemover)]
        public override IActionResult Delete(MunicipalityDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void Populate(MunicipalityInputViewModel modelInput)
        {
            var states = stateRepository.GetAll().OrderBy(entity => entity.RegionId);

            var selectListItems = new System.Collections.Generic.List<SelectListItem>();

            SelectListGroup currentGroup = null;

            foreach (var item in states)
            {
                if (currentGroup == null || currentGroup.Name != item.Region.Country.Name)
                {
                    currentGroup = new SelectListGroup { Name = item.Region.Country.Name };
                }

                selectListItems.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name, Group = currentGroup });
            }

            modelInput.States = selectListItems;
        }

        protected override void ConfigureCreate(MunicipalityInputViewModel modelInput)
        {
            Populate(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(MunicipalityInputViewModel modelInput)
        {
            Populate(modelInput);
        }

        protected override void ConfigureEdit(Municipality entity, MunicipalityInputViewModel modelInput)
        {
            Populate(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Municipality entity, MunicipalityInputViewModel modelInput)
        {
            Populate(modelInput);
        }

        protected override void PreCreate(Municipality entity, MunicipalityInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.StateId = modelInput.StateId;
            entity.State = stateRepository.Get(modelInput.StateId.Value);
        }

        protected override void PreEdit(Municipality entity, MunicipalityInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.State.Id != modelInput.StateId)
            {
                entity.StateId = modelInput.StateId;
                entity.State = stateRepository.Get(modelInput.StateId.Value);
            }
        }
    }
}
