namespace MOROPO.Framework.Guama.Identity.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractIdentity : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public AbstractIdentity()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public System.DateTime? BornDate { get; set; }

        public string About { get; set; }
    }
}
