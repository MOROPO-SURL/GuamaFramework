namespace MOROPO.Framework.Guama.Core.Presentation.Models.View
{
    public enum NotificationType
    {
        Success,
        Danger,
        Warning,
        Info
    }

    public class NotificationViewModel
    {
        public static string DEFAULT_NOTIFICATION_MAP_KEY = "default-notification";

        public NotificationType Type { get; set; }

        public System.DateTime Created { get; private set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public System.Collections.Generic.ICollection<string> Messages { get; set; }

        public NotificationViewModel()
        {
            Created = System.DateTime.Now;
        }
    }
}
