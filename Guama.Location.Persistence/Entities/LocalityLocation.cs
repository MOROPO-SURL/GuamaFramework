namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class LocalityLocation : Location
    {
        public Locality Locality { get; set; }

        public System.Guid? LocalityId { get; set; }
    }
}
