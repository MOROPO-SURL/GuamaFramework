using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities;
using MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Models.View;
using MOROPO.Framework.Guama.Core.Security.Presentation.Constants;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("security")]
    public class RolesController : Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Role, System.Guid, RoleInputViewModel, RoleDisplayViewModel>
    {
        private readonly IEntityRepository<Permission, int> permissionRepository;
        private readonly IRoleMemoryManagerFacade roleMemoryManager;

        public RolesController(IRoleMemoryManagerFacade roleMemoryManager, IEntityRepository<Permission, int> permissionRepository, IEntityRepository<Role, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<RolesController> localizer, ILogger<RolesController> logger)
            : base(repository, localizer, logger)
        {
            this.permissionRepository = permissionRepository;
            this.roleMemoryManager = roleMemoryManager;
        }

        [RequirePermissions(Permissions.RoleViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Permissions.RoleCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permissions.RoleCreator)]
        public override IActionResult Create(RoleInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Permissions.RoleEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permissions.RoleEditor)]
        public override IActionResult Edit(RoleInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Permissions.RoleRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permissions.RoleRemover)]
        public override IActionResult Delete(RoleDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateModelInputForAvailablePermissions(RoleInputViewModel modelInput, Role entity = null)
        {
            var unfamiliedPermissions = permissionRepository.FindBy(item => item.Family == null).OrderBy(item => item.Name).ToArray();

            modelInput.UngroupedSelectablePermissions = unfamiliedPermissions.Select(item => new Core.Presentation.Models.View.SelectionDescriptedItemInputViewModel<int>
            {
                Id = item.Id,
                Selected = entity != null && entity.PermissionBridges.Any(e => e.SternId.Equals(item.Id)),
                Label = item.Name,
                Description = item.Description
            }).ToArray();

            var familiedPermissionGroups = permissionRepository.FindBy(item => item.Family != null).GroupBy(item => item.Family).ToArray();

            modelInput.GroupedSelectablePermissions = familiedPermissionGroups.Select(item => new System.Collections.Generic.KeyValuePair<string, Core.Presentation.Models.View.SelectionDescriptedItemInputViewModel<int>[]>
            (
                item.Key,
                item.OrderBy(item => item.Name).Select(it3m => new Core.Presentation.Models.View.SelectionDescriptedItemInputViewModel<int>
                {
                    Id = it3m.Id,
                    Selected = entity != null && entity.PermissionBridges.Any(e => e.SternId.Equals(it3m.Id)),
                    Label = it3m.Name,
                    Description = it3m.Description
                }).ToArray()
            )).ToArray();
        }

        protected override void ConfigureCreate(RoleInputViewModel modelInput)
        {
            PopulateModelInputForAvailablePermissions(modelInput);
        }

        protected override void ConfigureEdit(Role entity, RoleInputViewModel modelInput)
        {
            PopulateModelInputForAvailablePermissions(modelInput, entity);
        }

        protected override void PreCreate(Role entity, RoleInputViewModel modelInput)
        {
            base.PostCreate(entity, modelInput);

            entity.PermissionBridges = modelInput.UngroupedSelectablePermissions.Where(item => item.Selected).Select(item => new RolePermissionBridge { SternId = item.Id, Prow = entity }).ToList();
        }

        protected override void PreEdit(Role entity, RoleInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

            entity.PermissionBridges.Clear();

            entity.PermissionBridges = modelInput.UngroupedSelectablePermissions.Where(item => item.Selected).Select(item => new RolePermissionBridge { SternId = item.Id, ProwId = entity.Id }).ToList();
        }

        protected override void PostCreate(Role entity, RoleInputViewModel modelInput)
        {
            base.PostCreate(entity, modelInput);

            roleMemoryManager.RememberRole(Repository.Get(entity.Id));
        }

        protected override void PostEdit(Role entity, RoleInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

            roleMemoryManager.ForguetRoleById(entity.Id);
            roleMemoryManager.RememberRole(Repository.Get(entity.Id));
        }

        protected override void PostDelete(Role entity)
        {
            base.PostDelete(entity);

            roleMemoryManager.ForguetRoleById(entity.Id);
        }
    }
}
