using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract
{
    [Microsoft.AspNetCore.Mvc.Route("management/[area]/[controller]")]
    [Microsoft.AspNetCore.Mvc.Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    public abstract class AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel> : Frontend.Controllers.Contract.AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputViewModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEntityManagementController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>> logger) 
            : base(commandRepository, queryRepository, localizer, logger)
        {
        }
    }
}
