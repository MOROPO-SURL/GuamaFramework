using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
	public abstract class AbstractArchivableEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IArchivableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()

    {
        public AbstractArchivableEntityAPIManagementController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, IStringLocalizer localizer, ILogger<AbstractArchivableEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) 
            : base(commandRepository, queryRepository, localizer, logger)
        {
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Archive(TId key)
        {
            try
            {
                var entity = commandRepository.Get(key);

                entity.Archived = true;

                commandRepository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPut("[action]/{key}")]
        public virtual ActionResult Unarchive(TId key)
        {
            try
            {
                var entity = commandRepository.Get(key);

                entity.Archived = false;

                commandRepository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }
    }
}

