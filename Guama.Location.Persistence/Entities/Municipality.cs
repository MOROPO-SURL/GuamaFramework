namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class Municipality : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public Municipality()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("StateId")]
        public State State { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? StateId { get; set; }
    }
}
