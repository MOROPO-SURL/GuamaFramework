namespace MOROPO.Framework.Guama.Identity.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractContactItem : Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public AbstractContactItem()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public string Label { get; set; }
    }
}
