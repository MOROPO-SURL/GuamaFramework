using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers
{
    [Route("[area]/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[area]/[controller]")]
    public abstract class AbstractEntityConsultFilteredController<TEntity, TId, TDisplayViewModel, TFilterModel> : AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, IEntityFilterModel<TEntity, TId>
    {
        public AbstractEntityConsultFilteredController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, Microsoft.Extensions.Logging.ILogger<AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>> logger)
            : base(repository, localizer, logger)
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
