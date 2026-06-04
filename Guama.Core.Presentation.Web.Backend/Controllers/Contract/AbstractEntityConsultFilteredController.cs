namespace MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract
{
    [Microsoft.AspNetCore.Mvc.Route("management/[area]/[controller]")]
    [Microsoft.AspNetCore.Mvc.Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    public abstract class AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel> : Frontend.Controllers.Contract.AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayViewModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityConsultFilteredController(Persistence.Repositories.IQueryEntityRepository<TEntity, TId> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel>> logger)
            : base(queryRepository, localizer, logger)
        {
        }
    }
}
