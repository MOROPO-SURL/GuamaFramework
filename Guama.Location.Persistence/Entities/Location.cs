namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public abstract class Location : Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public Location()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }
    }
}
