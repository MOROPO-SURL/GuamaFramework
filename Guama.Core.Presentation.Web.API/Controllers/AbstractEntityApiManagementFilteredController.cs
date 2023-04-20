using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers
{
    public abstract class AbstractEntityApiManagementFilteredController<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityApiManagementController<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityApiManagementFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, IStringLocalizer localizer, ILogger<AbstractEntityApiManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) : base(repository, localizer, logger)
        {
        }

        [HttpPost("filtered")]
        public virtual ActionResult<Models.PaginationModel<TDisplayModel>> GetFiltered([FromBody] TFilterModel filterModel)
        {
            return Ok(Repository.FindBy(filterModel.GetSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
        }

        [HttpPost]
        [Route("filtered/page/{sheet}")]
        [Route("filtered/page/{sheet}/limit/{limit}")]
        public virtual ActionResult<Models.PaginationModel<TDisplayModel>> GetPaginateFiltered([FromBody] TFilterModel filterModel, int sheet = 1, int limit = 25)
        {
            return BuildPaginationModelActionResult(sheet, limit, Repository.GetPagingOrderSelector(), filterModel);
        }
    }
}
