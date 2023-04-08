namespace MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Contract
{
    public interface IEntityInputModel<TEntity, TId> : IEntityModel<TEntity, TId> where TEntity : class, Entities.Contract.IEntity<TId>
    {
        void Mergue(TEntity entity);

        TEntity Export();

    }
}
