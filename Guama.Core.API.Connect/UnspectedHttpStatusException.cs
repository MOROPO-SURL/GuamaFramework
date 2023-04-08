namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect
{
    public class UnspectedHttpStatusException : System.Exception
    {
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }
}