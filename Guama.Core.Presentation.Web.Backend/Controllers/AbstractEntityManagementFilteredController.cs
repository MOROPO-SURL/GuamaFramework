namespace MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("management/[area]/[controller]")]
    [Microsoft.AspNetCore.Mvc.Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    public abstract class AbstractEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel> : Frontend.Controllers.AbstractEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputViewModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityManagementFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel>> logger) : base(repository, localizer, logger)
        {
        }
    }
}
