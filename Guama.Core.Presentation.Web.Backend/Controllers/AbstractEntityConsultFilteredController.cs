namespace MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("management/[area]/[controller]")]
    [Microsoft.AspNetCore.Mvc.Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    public abstract class AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel> : Frontend.Controllers.AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayViewModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityConsultFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>> logger)
            : base(repository, localizer, logger)
        {
        }
    }
}
