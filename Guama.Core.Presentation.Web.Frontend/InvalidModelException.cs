namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend
{
    public class InvalidModelException : System.Exception
    {
        public InvalidModelException(string modelKey, string errorMessage)
        {
            ModelKey = modelKey;
            ErrorMessage = errorMessage;
        }

        public string ModelKey { private set; get; }

        public string ErrorMessage { private set; get; }
    }
}
