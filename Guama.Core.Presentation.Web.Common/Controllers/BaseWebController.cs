using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.Controllers
{
    public abstract class BaseWebController : Controller
    {
        protected readonly IStringLocalizer localizer;
        protected readonly ILogger<BaseWebController> logger;

        public BaseWebController(IStringLocalizer localizer, ILogger<BaseWebController> logger)
        {
            this.localizer = localizer;
            this.logger = logger;
        }

        protected void AddNotification(NotificationType notificationType, string message, string discriminator = null)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Message = message
            };

            TempData[BuildNotificationMapKey(discriminator)] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void AddNotification(NotificationType notificationType, string title, string message, string discriminator = null)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Message = message
            };

            TempData[BuildNotificationMapKey(discriminator)] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetDefaultNotification(NotificationType notificationType, string[] messages, string discriminator = null)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Messages = messages
            };

            TempData[BuildNotificationMapKey(discriminator)] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetDefaultNotification(NotificationType notificationType, string title, string[] messages, string discriminator = null)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Messages = messages
            };

            TempData[BuildNotificationMapKey(discriminator)] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected void SetDefaultNotification(NotificationType notificationType, string title, string message, string[] messages, string discriminator = null)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Message = message,
                Messages = messages
            };

            TempData[BuildNotificationMapKey(discriminator)] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        private string BuildNotificationMapKey(string discriminator)
        {
            return string.Format("{0}-notification", discriminator ?? System.Guid.NewGuid().ToString());
        }
    }
}
