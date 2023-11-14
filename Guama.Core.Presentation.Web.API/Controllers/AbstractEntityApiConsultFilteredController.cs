using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers
{
    public abstract class AbstractEntityApiConsultFilteredController<TEntity, TId, TDisplayModel, TFilterModel> : AbstractEntityApiConsultController<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityApiConsultFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityApiConsultFilteredController<TEntity, TId, TDisplayModel, TFilterModel>> logger) : base(repository, localizer, logger)
        {
        }

        [HttpPost("filtered")]
        public virtual ActionResult<TDisplayModel[]> GetFiltered([FromBody] TFilterModel filterModel)
        {
            return Ok(Repository.FindBy(filterModel.GetSelector()).ToArray().Select(entity => EntityToDisplayModel(entity)).ToArray());
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
