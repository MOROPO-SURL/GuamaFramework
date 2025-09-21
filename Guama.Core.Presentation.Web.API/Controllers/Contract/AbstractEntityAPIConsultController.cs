using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEntityAPIConsultController<TEntity, TId, TDisplayModel> : Common.Controllers.BaseWebController
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        protected readonly Persistence.Repositories.IEntityRepository<TEntity, TId> Repository;

        public AbstractEntityAPIConsultController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEntityAPIConsultController<TEntity, TId, TDisplayModel>> logger) : base(localizer, logger)
        {
            Repository = repository;
        }

        protected virtual System.Func<TEntity, object> GetOrderSelector()
        {
            return e => e.Id;
        }

        protected virtual bool IsOrderDescending()
        {
            return false;
        }

        [HttpGet]
        public virtual ActionResult<TDisplayModel[]> Get()
        {
            var selector = GetGlobalSelector();

            if (selector != null)
            {
                return Ok(Repository.FindBy(selector).OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
            }

            return Ok(Repository.GetAll().OrderBy(GetOrderSelector()).Select(entity => EntityToDisplayModel(entity)).ToArray());
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<long> Count()
        {
            var globalSelector = GetGlobalSelector();

            if (globalSelector != null)
            {
                return Ok(Repository.Count(globalSelector));
            }

            return Ok(Repository.Count());
        }

        [HttpGet]
        [Route("page/{sheet}")]
        [Route("page/{sheet}/limit/{limit}")]
        public virtual ActionResult<Models.PaginationModel<TDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return BuildPaginationModelActionResult(sheet, limit, null);
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
            var model = new TDisplayModel();

            model.Import(entity);

            return model;
        }

        protected virtual System.Linq.Expressions.Expression<System.Func<TEntity, bool>> GetGlobalSelector() { return null; }

        protected ActionResult<Models.PaginationModel<TDisplayModel>> BuildPaginationModelActionResult(int page, int limit, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId> filterModel)
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

                if (!IsOrderDescending())
                {
                    entities = selection.Skip((page - 1) * limit).Take(limit).OrderBy(GetOrderSelector()).ToList();
                }
                else
                {
                    entities = selection.Skip((page - 1) * limit).Take(limit).OrderByDescending(GetOrderSelector()).ToList();
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
