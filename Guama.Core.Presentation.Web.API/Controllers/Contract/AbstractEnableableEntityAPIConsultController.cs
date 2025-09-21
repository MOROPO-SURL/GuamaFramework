using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Extensions;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public class AbstractEnableableEntityAPIConsultController<TEntity, TId, TDisplayModel> : AbstractEntityAPIConsultController<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEnableableEntityAPIConsultController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer<AbstractEnableableEntityAPIConsultController<TEntity, TId, TDisplayModel>> localizer, Microsoft.Extensions.Logging.ILogger<AbstractEnableableEntityAPIConsultController<TEntity, TId, TDisplayModel>> logger) : base(repository, localizer, logger)
        {
        }

        [HttpGet]
        [Route("enabled")]
        public virtual ActionResult<TDisplayModel[]> GetEnabled()
        {
            var selector = GetGlobalSelector().CombineForAndCondition(entity => entity.Enabled);

            if (selector != null)
            {
                return Ok(Repository.FindBy(selector).OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
            }

            return Ok(Repository.GetAll().OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
        }

        [HttpGet]
        [Route("disabled")]
        public virtual ActionResult<TDisplayModel[]> GetDisabled()
        {
            var selector = GetGlobalSelector().CombineForAndCondition(entity => !entity.Enabled);

            if (selector != null)
            {
                return Ok(Repository.FindBy(selector).OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
            }

            return Ok(Repository.GetAll().OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
        }
    }
}
