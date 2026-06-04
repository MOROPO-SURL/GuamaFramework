using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers.Contract
{
    public abstract class AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel> : AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>
        where TEntity : class, IEntity<TId>
        where TInputViewModel : class, IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
    {
        protected readonly ICommandEntityRepository<TEntity, TId> commandRepository;

        public AbstractEntityManagementController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>> logger)
            : base(queryRepository, localizer, logger)
        {
            this.commandRepository = commandRepository;
        }

        protected virtual void ConfigureCreate(TInputViewModel model) { }

        [HttpGet]
        [Route("[action]")]
        public virtual IActionResult Create()
        {
            try
            {
                var model = new TInputViewModel();

                ConfigureCreate(model);

                return View(model);
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

        protected virtual void PreCreate(TEntity entity, TInputViewModel model) { }

        protected virtual void PostCreate(TEntity entity, TInputViewModel model) { }

        protected virtual void ConfigurePostFailCreateValidation(TInputViewModel model) { }

        protected virtual void ValidateCreate(TInputViewModel model) { }

        protected virtual void ValidateEdit(TInputViewModel model) { }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(TInputViewModel model)
        {
            try
            {
                ValidateCreate(model);

                try
                {
                    if (ModelState.IsValid)
                    {
                        var entity = model.Export();

                        PreCreate(entity, model);

                        commandRepository.Add(entity, User.Identity.Name);

                        PostCreate(entity, model);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return GetSuccessCreateActionResult(entity.Id);
                    }
                }
                catch (InvalidModelException e)
                {
                    ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                }
                catch (System.Exception)
                {
                    throw;
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                ConfigurePostFailCreateValidation(model);

                return View(model);
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

        protected virtual void ConfigureEdit(TEntity entity, TInputViewModel model) { }

        [HttpGet]
        [Route("[action]/{key}")]
        public virtual IActionResult Edit(TId key)
        {
            try
            {
                var entity = queryRepository.Get(key);

                var model = new TInputViewModel();

                model.Import(entity);

                ConfigureEdit(entity, model);

                return View(model);
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return GetDefaultActionResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        protected virtual void PreEdit(TEntity entity, TInputViewModel model) { }

        protected virtual void PostEdit(TEntity entity, TInputViewModel model) { }

        protected virtual void ConfigurePostFailEditValidation(TEntity entity, TInputViewModel model) { }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(TInputViewModel model)
        {
            try
            {
                ValidateEdit(model);

                var entity = commandRepository.Get(model.Id);

                if (ModelState.IsValid)
                {
                    try
                    {
                        model.Mergue(entity);

                        try
                        {
                            PreEdit(entity, model);
                        }
                        catch (InvalidModelException e)
                        {
                            ModelState.AddModelError(e.ModelKey, e.ErrorMessage);

                            throw new System.InvalidOperationException(e.ErrorMessage, e);
                        }

                        commandRepository.Update(entity, User.Identity.Name);

                        PostEdit(entity, model);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                        return GetSuccessEditActionResult(entity.Id);
                    }

                    catch (System.Exception e)
                    {
                        if (!(e.InnerException is InvalidModelException))
                        {
                            logger.LogError(e.InnerException, e.Message);

                            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                            return GetDefaultActionResult();
                        }
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                ConfigurePostFailEditValidation(entity, model);

                return View(model);
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

        [HttpGet]
        [Route("[action]/{key}")]
        public virtual IActionResult Delete(TId key)
        {
            try
            {
                var entity = queryRepository.Get(key);

                var model = new TDisplayViewModel();

                model.Import(entity);

                PopulateViewModelDisplayForDetail(model, entity);

                return View(model);
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return GetDefaultActionResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        protected virtual void PreDelete(TEntity entity) { }

        protected virtual void PostDelete(TEntity entity) { }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Delete(TDisplayViewModel model)
        {
            try
            {
                var entity = queryRepository.Get(model.Id);

                PreDelete(entity);

                commandRepository.Delete(entity, User.Identity.Name);

                PostDelete(entity);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return GetSuccessDeleteActionResult();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return GetSuccessDeleteActionResult();
            }
            catch (Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return GetDefaultActionResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return View(model);
        }

        protected virtual IActionResult GetSuccessCreateActionResult(TId key)
        {
            return GetDefaultActionResult();
        }

        protected virtual IActionResult GetSuccessEditActionResult(TId key)
        {
            return RedirectToAction("edit", new { key });
        }

        protected virtual IActionResult GetSuccessDeleteActionResult()
        {
            return GetDefaultActionResult();
        }

        protected virtual string GetCreationSuccessMessage()
        {
            return "CreationSuccessMessage";
        }

        protected virtual string GetEditionSuccessMessage()
        {
            return "EditionSuccessMessage";
        }

        protected virtual string GetDeletionSuccessMessage()
        {
            return "DeletionSuccessMessage";
        }

        protected virtual string GetInvalidMessage()
        {
            return "InvalidMessage";
        }

        protected virtual string GetOnDeletionDependecyConstrainErrorMessage()
        {
            return "DeletionDependecyConstrainErrorMessage";
        }
    }
}
