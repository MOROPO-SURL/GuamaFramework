namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
