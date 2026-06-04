using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEnableableEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEnableableEntityAPIManagementFilteredController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, IStringLocalizer localizer, ILogger<AbstractEnableableEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>> logger) 
            : base(commandRepository, queryRepository, localizer, logger)
        {
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Enable(TId key)
        {
            try
            {
                var entity = commandRepository.Get(key);

                entity.Enabled = true;

                commandRepository.Update(entity, User.Identity.Name);

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
                var entity = commandRepository.Get(key);

                entity.Enabled = false;

                commandRepository.Update(entity, User.Identity.Name);

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
