namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class Locality : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public Locality()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("MunicipalityId")]
        public Municipality Municipality { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? MunicipalityId { get; set; }
    }
}
