namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.Exceptions
{
    public class NoContinuableActionException : System.Exception
    {
        public NoContinuableActionException(string message) : base(message)
        {
        }

        public Models.View.NotificationType Type { get; set; }

        public string Title { get; set; }
    }
}
