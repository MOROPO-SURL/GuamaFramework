using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
	public abstract class AbstractArchivableEnableableEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEnableableEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IArchivableEntity<TId>, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractArchivableEnableableEntityAPIManagementFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, IStringLocalizer localizer, ILogger<AbstractArchivableEnableableEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>> logger) : base(repository, localizer, logger)
        {
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Archive(TId key)
        {
            try
            {
                var entity = queryRepository.Get(key);

                entity.Archived = true;

                queryRepository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);

                return NoContent();
            }
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Unarchive(TId key)
        {
            try
            {
                var entity = queryRepository.Get(key);

                entity.Archived = false;

                queryRepository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);

                return NoContent();
            }
        }
    }
}

