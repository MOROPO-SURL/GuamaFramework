namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface INamedEntity<TId> : IEntity<TId>
    {
        string Name { get; set; }
    }
}