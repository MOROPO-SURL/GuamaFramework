namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PostalContactItem : Contract.AbstractContactItem
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Address { get; set; }

        public Location.Persistence.Entities.Location Location { get; set; }
        public System.Guid? LocationId { get; set; }

        public int ZipCode { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
