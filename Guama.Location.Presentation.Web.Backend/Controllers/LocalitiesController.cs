using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Location.Persistence.Entities;
using MOROPO.Framework.Guama.Location.Presentation.Constants;
using MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.Filter;
using MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View;
using System.Linq;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Controllers;

[Microsoft.AspNetCore.Authorization.Authorize]
[Area("location")]
public class LocalitiesController : Core.Presentation.Web.Backend.Controllers.Contract.AbstractEntityManagementFilteredController<Locality, System.Guid, LocalityInputViewModel, LocalityDisplayViewModel, LocalityFilterModel>
{
    private readonly IQueryEntityRepository<Municipality, System.Guid> municipalityQueryRepository;

    public LocalitiesController(IQueryEntityRepository<Municipality, System.Guid> municipalityQueryRepository, ICommandEntityRepository<Locality, System.Guid> commandRepository, IQueryEntityRepository<Locality, System.Guid> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer<LocalitiesController> localizer, Microsoft.Extensions.Logging.ILogger<LocalitiesController> logger) 
        : base(commandRepository, queryRepository, localizer, logger)
    {
        this.municipalityQueryRepository = municipalityQueryRepository;
    }

    [RequirePermissions(Permissions.LocalityViewer)]
    public override IActionResult Index(int sheet = 1, int limit = 25)
    {
        return base.Index(sheet, limit);
    }

    protected override System.Func<Locality, object> GetOrderSelector()
    {
        return entity => entity.Name;
    }

    protected override string GetFilterSessionID()
    {
        return "guamaframework-location-backend-backend-localities-filter";
    }

    [RequirePermissions(Permissions.LocalityViewer)]
    public override IActionResult Filter(LocalityFilterModel filterModel)
    {
        return base.Filter(filterModel);
    }

    [RequirePermissions(Permissions.LocalityCreator)]
    public override IActionResult Create()
    {
        return base.Create();
    }

    [RequirePermissions(Permissions.LocalityCreator)]
    public override IActionResult Create(LocalityInputViewModel modelInput)
    {
        return base.Create(modelInput);
    }

    [RequirePermissions(Permissions.LocalityEditor)]
    public override IActionResult Edit(System.Guid key)
    {
        return base.Edit(key);
    }

    [RequirePermissions(Permissions.LocalityEditor)]
    public override IActionResult Edit(LocalityInputViewModel modelInput)
    {
        return base.Edit(modelInput);
    }

    [RequirePermissions(Permissions.LocalityRemover)]
    public override IActionResult Delete(System.Guid key)
    {
        return base.Delete(key);
    }

    [RequirePermissions(Permissions.LocalityRemover)]
    public override IActionResult Delete(LocalityDisplayViewModel displayModel)
    {
        return base.Delete(displayModel);
    }

    private void Populate(LocalityInputViewModel modelInput)
    {
        var municipalities = municipalityQueryRepository.GetAll().OrderBy(entity => entity.State.Id);

        var selectListItems = new System.Collections.Generic.List<SelectListItem>();

        SelectListGroup currentGroup = null;

        foreach (var item in municipalities)
        {
            var groupName = item.State.Name + ", " + item.State.Region.Country.Name;

            if (currentGroup == null || currentGroup.Name != groupName)
            {
                currentGroup = new SelectListGroup { Name = groupName };
            }

            selectListItems.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name, Group = currentGroup });
        }

        modelInput.Municipalities = selectListItems;
    }

    protected override void ConfigureCreate(LocalityInputViewModel modelInput)
    {
        Populate(modelInput);
    }

    protected override void ConfigurePostFailCreateValidation(LocalityInputViewModel modelInput)
    {
        Populate(modelInput);
    }

    protected override void ConfigureEdit(Locality entity, LocalityInputViewModel modelInput)
    {
        Populate(modelInput);
    }

    protected override void ConfigurePostFailEditValidation(Locality entity, LocalityInputViewModel modelInput)
    {
        Populate(modelInput);
    }

    protected override void PreCreate(Locality entity, LocalityInputViewModel modelInput)
    {
        base.PreCreate(entity, modelInput);

        entity.MunicipalityId = modelInput.MunicipalityId;
        entity.Municipality = municipalityQueryRepository.Get(modelInput.MunicipalityId.Value);
    }

    protected override void PreEdit(Locality entity, LocalityInputViewModel modelInput)
    {
        base.PreEdit(entity, modelInput);

        if (entity.Municipality.Id != modelInput.MunicipalityId)
        {
            entity.MunicipalityId = modelInput.MunicipalityId;
            entity.Municipality = municipalityQueryRepository.Get(modelInput.MunicipalityId.Value);
        }
    }
}
