using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using System;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers.Contract
{
    public abstract class AbstractEnableableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel> : AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>
        where TEntity : class, IEnableableEntity<TId>
        where TInputViewModel : class, IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEnableableEntityManagementController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEnableableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>> logger)
            : base(commandRepository, queryRepository, localizer, logger)
        {
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Enable(TId key)
        {
            try
            {
                var entity = commandRepository.Get(key);

                entity.Enabled = true;

                commandRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Presentation.Models.View.NotificationType.Success, GetEnableSuccessMessage());
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());
            }

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Disable(TId key)
        {
            try
            {
                var entity = commandRepository.Get(key);

                entity.Enabled = false;

                commandRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Presentation.Models.View.NotificationType.Success, GetDisableSuccessMessage());
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());
            }

            return GetDefaultActionResult();
        }

        protected virtual string GetEnableSuccessMessage()
        {
            return "EnableSuccessMessage";
        }

        protected virtual string GetDisableSuccessMessage()
        {
            return "DisableSuccessMessage";
        }
    }
}
