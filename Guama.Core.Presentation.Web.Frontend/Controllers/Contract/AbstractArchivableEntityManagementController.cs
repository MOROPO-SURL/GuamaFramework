using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using System;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers.Contract
{
	public abstract class AbstractArchivableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel> : AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>
        where TEntity : class, IArchivableEntity<TId>
        where TInputViewModel : class, IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractArchivableEntityManagementController(IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractArchivableEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>> logger)
            : base(repository, localizer, logger)
        {
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Archive(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                entity.Archived = true;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Presentation.Models.View.NotificationType.Success, GetArchiveSuccessMessage());
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
        public virtual IActionResult Unarchive(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                entity.Archived = false;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Presentation.Models.View.NotificationType.Success, GetUnarchiveSuccessMessage());
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

        protected virtual string GetArchiveSuccessMessage()
        {
            return "ArchiveSuccessMessage";
        }

        protected virtual string GetUnarchiveSuccessMessage()
        {
            return "UnarchiveSuccessMessage";
        }
    }
}

