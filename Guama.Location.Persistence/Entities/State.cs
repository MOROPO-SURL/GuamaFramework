namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class State : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public State()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("RegionId")]
        public Region Region { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? RegionId { get; set; }
    }
}
