namespace MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract
{
    public interface IEntityModel<in TEntity, TId> where TEntity : class, Entities.Contract.IEntity<TId>
    {
        TId Id { get; set; }

        void Import(TEntity entity);
    }
}
