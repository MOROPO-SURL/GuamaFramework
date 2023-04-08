namespace MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract
{
    public interface IEntityDisplayModel<in TEntity, TId> : IEntityModel<TEntity, TId> where TEntity : class, Entities.Contract.IEntity<TId>
    {
    }
}
