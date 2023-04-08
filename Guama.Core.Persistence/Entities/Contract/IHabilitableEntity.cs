namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IHabilitableEntity<TId> : IEntity<TId>
    {
        bool Enabled { get; set; }
    }
}
