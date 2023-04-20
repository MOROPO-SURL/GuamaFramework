using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers
{
    public abstract class AbstractEntityApiConsultController<TEntity, TId, TDisplayModel> : Common.Controllers.BaseWebController
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        protected readonly Persistence.Repositories.IEntityRepository<TEntity, TId> Repository;

        public AbstractEntityApiConsultController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEntityApiConsultController<TEntity, TId, TDisplayModel>> logger) : base(localizer, logger)
        {
            Repository = repository;
        }

        [HttpGet]
        public virtual ActionResult<TDisplayModel[]> Get()
        {
            return Ok(Repository.GetAll().Select(entity => EntityToDisplayModel(entity)).ToArray());
        }

        [HttpGet]
        [Route("page/{sheet}")]
        [Route("page/{sheet}/limit/{limit}")]
        public virtual ActionResult<Models.PaginationModel<TDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return BuildPaginationModelActionResult(sheet, limit, Repository.GetPagingOrderSelector(), null);
        }

        [HttpGet("{key}")]
        public virtual ActionResult<TDisplayModel> Get(TId key)
        {
            try
            {
                return Ok(EntityToDisplayModel(Repository.Get(key)));
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);

                return NotFound();
            }
        }

        protected virtual TDisplayModel EntityToDisplayModel(TEntity entity)
        {
            var displayModel = new TDisplayModel();

            displayModel.Import(entity);

            return displayModel;
        }

        protected virtual System.Linq.Expressions.Expression<System.Func<TEntity, bool>> GetGlobalSelector() { return null; }

        protected ActionResult<Models.PaginationModel<TDisplayModel>> BuildPaginationModelActionResult<TOrderingKey>(int page, int limit, System.Func<TEntity, TOrderingKey> ordering, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId> filterModel)
        {
            IQueryable<TEntity> selection = null;

            var globalSelector = GetGlobalSelector();
            var filterSelector = filterModel?.GetSelector();

            if (globalSelector != null)
            {
                if (filterModel != null)
                {
                    selection = Repository.FindBy(globalSelector).Where(filterSelector);
                }
                else
                {
                    selection = Repository.FindBy(globalSelector);
                }
            }
            else
            {
                if (filterModel != null)
                {
                    selection = Repository.FindBy(filterSelector);
                }
                else
                {
                    selection = Repository.GetAll();
                }
            }

            var maxCount = selection.Count();

            var paginationModel = new Models.PaginationModel<TDisplayModel>
            {
                Limit = limit,
                Page = 0,
                HasNext = false,
                HasPrevious = false,
                List = new System.Collections.Generic.List<TDisplayModel>()
            };

            if (maxCount > 0)
            {
                var rest = maxCount % limit;

                var maxPageCount = maxCount / limit;

                if (rest > 0)
                    maxPageCount += 1;

                if (page < 0 || page > maxPageCount)
                {
                    page = 1;
                }

                var entities = new System.Collections.Generic.List<TEntity>();

                if (!Repository.IsPagingOrderDescending())
                {
                    entities = selection.OrderBy(ordering).Skip((page - 1) * limit).Take(limit).ToList();
                }
                else
                {
                    entities = selection.OrderByDescending(ordering).Skip((page - 1) * limit).Take(limit).ToList();
                }

                var models = entities.Select(entity => EntityToDisplayModel(entity)).ToList();

                paginationModel = new Models.PaginationModel<TDisplayModel>
                {
                    Limit = limit,
                    Page = page,
                    HasPrevious = page > 1,
                    HasNext = page < maxPageCount,
                    MaxPageCount = maxPageCount,
                    List = models
                };
            }

            return Ok(paginationModel);
        }
    }
}
