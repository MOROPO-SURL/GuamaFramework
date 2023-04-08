namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class Region : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public Region()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CountryId")]
        public Country Country { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? CountryId { get; set; }
    }
}