namespace MOROPO.Framework.Guama.Core.Persistence.Repositories
{
    internal interface IEntityRepository<TEntity, TId> : IQueryEntityRepository<TEntity, TId>, ICommandEntityRepository<TEntity, TId>
        where TEntity : class, Entities.Contract.IEntity<TId>
    {
    }
}
