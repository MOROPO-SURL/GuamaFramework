namespace MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract
{
    [Microsoft.AspNetCore.Mvc.Route("management/[area]/[controller]")]
    [Microsoft.AspNetCore.Mvc.Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    public class AbstractArchivableEnableableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel> : Frontend.Controllers.Contract.AbstractArchivableEnableableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IArchivableEntity<TId>, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TDisplayViewModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TInputViewModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
    {
        public AbstractArchivableEnableableEntityManagementController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractArchivableEnableableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>> logger) : base(repository, localizer, logger)
        {
        }
    }
}

