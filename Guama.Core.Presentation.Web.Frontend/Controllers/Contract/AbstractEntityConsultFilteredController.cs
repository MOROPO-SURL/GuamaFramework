using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers.Contract
{
    [Route("[area]/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[area]/[controller]")]
    public abstract class AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel> : AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityConsultFilteredController(Persistence.Repositories.IQueryEntityRepository<TEntity, TId> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel>> logger)
            : base(queryRepository, localizer, logger)
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
