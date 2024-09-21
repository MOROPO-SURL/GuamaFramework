namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IEnableableEntity<TId> : IEntity<TId>
    {
        bool Enabled { get; set; }
    }
}
