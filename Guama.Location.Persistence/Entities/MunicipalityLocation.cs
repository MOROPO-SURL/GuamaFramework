namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class MunicipalityLocation : Location
    {
        public Municipality Municipality { get; set; }

        public System.Guid? MunicipalityId { get; set; }
    }
}
