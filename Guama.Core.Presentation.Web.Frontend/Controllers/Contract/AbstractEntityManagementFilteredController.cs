using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers.Contract
{
    public abstract class AbstractEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel> : AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputViewModel : class, IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityManagementFilteredController(IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEntityManagementFilteredController<TEntity, TId, TInputViewModel, TDisplayViewModel, TFilterModel>> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            try
            {
                return BuildActionResultWithPaginationViewModel(sheet, limit, GetFilterModel());
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
        public virtual IActionResult Filter(TFilterModel model)
        {
            try
            {
                UpdateFilter(model);

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

        protected virtual void UpdateFilter(TFilterModel model)
        {
            if (model != null)
            {
                HttpContext.Session.SetString(GetFilterSessionID(), Newtonsoft.Json.JsonConvert.SerializeObject(model));
            }
        }
    }
}
