using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers
{
    public abstract class AbstractHabilitableEntityApiManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityApiManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IHabilitableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractHabilitableEntityApiManagementFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, IStringLocalizer localizer, ILogger<AbstractEntityApiManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) : base(repository, localizer, logger)
        {
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Enable(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                entity.Enabled = true;

                Repository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);

                return NoContent();
            }
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Disable(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                entity.Enabled = false;

                Repository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);

                return NoContent();
            }
        }
    }
}
