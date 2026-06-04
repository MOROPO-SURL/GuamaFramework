namespace MOROPO.Framework.Guama.Core.Persistence.Repositories
{
    public interface ICommandEntityRepository<TEntity, TId> where TEntity : class, Entities.Contract.IEntity<TId>
    {
        TEntity Get(TId id);

        TEntity Add(TEntity entity, string author, bool commit = true);

        TEntity Update(TEntity entity, string author, bool commit = true);

        void Delete(TEntity entity, string author, bool commit = true);
    }
}