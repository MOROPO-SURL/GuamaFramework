using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEnableableEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()

    {
        public AbstractEnableableEntityAPIManagementController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, IStringLocalizer localizer, ILogger<AbstractEnableableEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) : base(repository, localizer, logger)
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
            catch
            {
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
            catch
            {
                return NoContent();
            }
        }
    }
}
