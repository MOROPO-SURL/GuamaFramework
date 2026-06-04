using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEntityAPIManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityAPIManagementFilteredController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, IStringLocalizer localizer, ILogger<AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) 
            : base(commandRepository, queryRepository, localizer, logger)
        {
        }

        [HttpPost("filtered")]
        public virtual ActionResult<TDisplayModel[]> GetFiltered([FromBody] TFilterModel filterModel)
        {
            return Ok(queryRepository.FindBy(filterModel.GetSelector()).ToArray().Select(entity => EntityToDisplayModel(entity)).ToArray());
        }

        [HttpPost]
        [Route("filtered/page/{sheet}")]
        [Route("filtered/page/{sheet}/limit/{limit}")]
        public virtual ActionResult<Models.PaginationModel<TDisplayModel>> GetPaginateFiltered([FromBody] TFilterModel filterModel, int sheet = 1, int limit = 25)
        {
            return BuildPaginationModelActionResult(sheet, limit, filterModel);
        }
    }
}
