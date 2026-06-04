using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Extensions;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEnableableEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()

    {
        public AbstractEnableableEntityAPIManagementController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEnableableEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) 
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
                var entity = commandRepository.Get(key);

                entity.Enabled = false;

                commandRepository.Update(entity, User.Identity.Name);

                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("enabled")]
        public virtual ActionResult<TDisplayModel[]> GetEnabled()
        {
            var selector = GetGlobalSelector().CombineForAndCondition(entity => entity.Enabled);

            if (selector != null)
            {
                return Ok(queryRepository.FindBy(selector).OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
            }

            return Ok(queryRepository.GetAll().OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
        }

        [HttpGet]
        [Route("disabled")]
        public virtual ActionResult<TDisplayModel[]> GetDisabled()
        {
            var selector = GetGlobalSelector().CombineForAndCondition(entity => !entity.Enabled);

            if (selector != null)
            {
                return Ok(queryRepository.FindBy(selector).OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
            }

            return Ok(queryRepository.GetAll().OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
        }
    }
}
