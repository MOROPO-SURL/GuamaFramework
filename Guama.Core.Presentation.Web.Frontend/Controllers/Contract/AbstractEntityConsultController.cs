using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Controllers.Contract
{
    [Route("[area]/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[area]/[controller]")]
    public abstract class AbstractEntityConsultController<TEntity, TId, TDisplayViewModel> : Common.Controllers.BaseWebController
        where TEntity : class, IEntity<TId>
        where TDisplayViewModel : class, IEntityDisplayModel<TEntity, TId>, new()
    {
        protected readonly IEntityRepository<TEntity, TId> Repository;

        public AbstractEntityConsultController(IEntityRepository<TEntity, TId> repository, Microsoft.Extensions.Localization.IStringLocalizer localizer, ILogger<AbstractEntityConsultController<TEntity, TId, TDisplayViewModel>> logger)
            : base(localizer, logger)
        {
            Repository = repository;
        }

        protected virtual void PopulateViewModelDisplayForList(TDisplayViewModel model, TEntity entity) { }

        protected virtual void PopulateViewModelDisplayForDetail(TDisplayViewModel model, TEntity entity) { }

        protected virtual System.Func<TEntity, object> GetOrderSelector()
        {
            return e => e.Id;
        }

        protected virtual bool IsOrderDescending()
        {
            return false;
        }

        [HttpGet]
        [Route("")]
        [Route("page/{sheet}")]
        [Route("page/{sheet}/limit/{limit}")]
        public virtual IActionResult Index(int sheet = 1, int limit = 25)
        {
            try
            {
                return BuildActionResultWithPaginationViewModel(sheet, limit, null);
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
        [Route("{key}")]
        public virtual IActionResult Detail(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new TDisplayViewModel();

                model.Import(entity);

                PopulateViewModelDisplayForDetail(model, entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        protected virtual string GetNotFoundMessage()
        {
            return "NotFoundMessage";
        }

        protected virtual IActionResult GetDefaultActionResult()
        {
            return RedirectToAction("index");
        }

        protected bool HasDefaultNotification { get => TempData.ContainsKey(NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY) && TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] != null; }

        protected void SetDefaultNotification(NotificationType notificationType, string message)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Message = message
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetNextNotification(NotificationType notificationType, string title, string message)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Message = message
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetDefaultNotification(NotificationType notificationType, string[] messages)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Messages = messages
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetDefaultNotification(NotificationType notificationType, string title, string[] messages)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Messages = messages
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetDefaultNotification(NotificationType notificationType, string title, string message, string[] messages)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Message = message,
                Messages = messages
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected virtual System.Linq.Expressions.Expression<System.Func<TEntity, bool>> GetGlobalSelector() { return null; }

        protected IActionResult BuildActionResultWithPaginationViewModel(int page, int limit, IEntityFilterModel<TEntity, TId> filter)
        {
            IQueryable<TEntity> selection = null;

            var globalSelector = GetGlobalSelector();
            var filterSelector = filter?.GetSelector();

            if (globalSelector != null)
            {
                if (filter != null)
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
                if (filter != null)
                {
                    selection = Repository.FindBy(filterSelector);
                }
                else
                {
                    selection = Repository.GetAll();
                }
            }

            try
            {
                var maxCount = selection.Count();

                var model = new Models.View.PaginationViewModel<TEntity, TId, TDisplayViewModel>
                {
                    Limit = limit,
                    Page = 0,
                    HasNext = false,
                    HasPrevious = false,
                    Filter = filter,
                    List = new List<TDisplayViewModel>()
                };

                if (maxCount > 0)
                {
                    var rest = maxCount % limit;

                    var maxPageCount = maxCount / limit;

                    if (rest > 0)
                        maxPageCount += 1;

                    if (page < 0 || page > maxPageCount)
                    {
                        throw new System.InvalidOperationException();
                    }

                    var entities = new List<TEntity>();

                    if (!IsOrderDescending())
                    {
                        entities = selection.Skip((page - 1) * limit).Take(limit).OrderBy(GetOrderSelector()).ToList();
                    }
                    else
                    {
                        entities = selection.Skip((page - 1) * limit).Take(limit).OrderByDescending(GetOrderSelector()).ToList();
                    }

                    var models = new List<TDisplayViewModel>();

                    foreach (var item in entities)
                    {
                        var itemModelDisplay = new TDisplayViewModel();

                        itemModelDisplay.Import(item);

                        PopulateViewModelDisplayForList(itemModelDisplay, item);

                        models.Add(itemModelDisplay);
                    }

                    model = new Models.View.PaginationViewModel<TEntity, TId, TDisplayViewModel>
                    {
                        Limit = limit,
                        Page = page,
                        HasPrevious = page > 1,
                        HasNext = page < maxPageCount,
                        MaxPageCount = maxPageCount,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        RouteValues = ControllerContext.ActionDescriptor.RouteValues,
                        Filter = filter,
                        List = models
                    };
                }

                return View(model);
            }
            catch (System.InvalidOperationException e)
            {
                logger.LogError(e.InnerException, e.Message);

                return RedirectToAction(ControllerContext.ActionDescriptor.ActionName, new { key = "" });
            }
        }
    }
}
