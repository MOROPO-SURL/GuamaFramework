using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers
{
    public abstract class AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel> : AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>
        where TEntity : class, IEntity<TId>
        where TInputViewModel : class, IEntityInputModel<TEntity, TId>, new()
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEntityManagementController(IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEntityManagementController<TEntity, TId, TInputViewModel, TDisplayViewModel>> logger)
            : base(repository, localizer, logger)
        {
        }

        protected virtual void ConfigureCreate(TInputViewModel modelInput) { }

        [HttpGet]
        [Route("[action]")]
        public virtual IActionResult Create()
        {
            try
            {
                var modelInput = new TInputViewModel();

                ConfigureCreate(modelInput);

                return View(modelInput);
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

        protected virtual void PreCreate(TEntity entity, TInputViewModel modelInput) { }

        protected virtual void PostCreate(TEntity entity, TInputViewModel modelInput) { }

        protected virtual void ConfigurePostFailCreateValidation(TInputViewModel modelInput) { }

        protected virtual void ValidateCreate(TInputViewModel modelInput) { }

        protected virtual void ValidateEdit(TInputViewModel modelInput) { }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(TInputViewModel modelInput)
        {
            try
            {
                ValidateCreate(modelInput);

                try
                {
                    if (ModelState.IsValid)
                    {
                        var entity = modelInput.Export();

                        PreCreate(entity, modelInput);

                        Repository.Add(entity, User.Identity.Name);

                        PostCreate(entity, modelInput);

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

                ConfigurePostFailCreateValidation(modelInput);

                return View(modelInput);
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

        protected virtual void ConfigureEdit(TEntity entity, TInputViewModel modelInput) { }

        [HttpGet]
        [Route("[action]/{key}")]
        public virtual IActionResult Edit(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                var modelInput = new TInputViewModel();

                modelInput.Import(entity);

                ConfigureEdit(entity, modelInput);

                return View(modelInput);
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

        protected virtual void PreEdit(TEntity entity, TInputViewModel modelInput) { }

        protected virtual void PostEdit(TEntity entity, TInputViewModel modelInput) { }

        protected virtual void ConfigurePostFailEditValidation(TEntity entity, TInputViewModel modelInput) { }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(TInputViewModel modelInput)
        {
            try
            {
                ValidateEdit(modelInput);

                var entity = Repository.Get(modelInput.Id);

                if (ModelState.IsValid)
                {
                    try
                    {
                        modelInput.Mergue(entity);

                        try
                        {
                            PreEdit(entity, modelInput);
                        }
                        catch (InvalidModelException e)
                        {
                            ModelState.AddModelError(e.ModelKey, e.ErrorMessage);

                            throw new System.InvalidOperationException(e.ErrorMessage, e);
                        }

                        Repository.Update(entity, User.Identity.Name);

                        PostEdit(entity, modelInput);

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

                ConfigurePostFailEditValidation(entity, modelInput);

                return View(modelInput);
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
                var entity = Repository.Get(key);

                var modelDisplay = new TDisplayViewModel();

                modelDisplay.Import(entity);

                PopulateViewModelDisplayExtras(modelDisplay, entity);

                return View(modelDisplay);
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
        public virtual IActionResult Delete(TDisplayViewModel displayModel)
        {
            try
            {
                var entity = Repository.Get(displayModel.Id);

                PreDelete(entity);

                Repository.Delete(entity, User.Identity.Name);

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

            return View(displayModel);
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

        protected virtual string GetNotFoundMessage()
        {
            return "NotFoundMessage";
        }

        protected virtual string GetOnDeletionDependecyConstrainErrorMessage()
        {
            return "DeletionDependecyConstrainErrorMessage";
        }
    }
}
