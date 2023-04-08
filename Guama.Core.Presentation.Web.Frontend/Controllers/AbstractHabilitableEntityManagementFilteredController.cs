using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers
{
    public abstract class AbstractHabilitableEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel> : AbstractHabilitableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IHabilitableEntity<TId>
        where TInputViewModel : class, IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, IEntityFilterModel<TEntity, TId>
    {
        public AbstractHabilitableEntityManagementFilteredController(IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractHabilitableEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel>> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            try
            {
                return BuildActionResultWithPaginationViewModel(sheet, limit, Repository.GetPagingOrderSelector(), GetFilterModel());
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return GetDefaultActionResult();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Filter(TFilterModel filterModel)
        {
            try
            {
                UpdateFilter(filterModel);

                return GetDefaultActionResult();
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return GetDefaultActionResult();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        protected abstract string GetFilterSessionID();

        protected virtual TFilterModel GetFilterModel()
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TFilterModel>(HttpContext.Session.GetString(GetFilterSessionID()));
            }
            catch { }

            return null;
        }

        protected virtual void UpdateFilter(TFilterModel filterModel)
        {
            if (filterModel != null)
            {
                HttpContext.Session.SetString(GetFilterSessionID(), Newtonsoft.Json.JsonConvert.SerializeObject(filterModel));
            }
        }
    }
}
