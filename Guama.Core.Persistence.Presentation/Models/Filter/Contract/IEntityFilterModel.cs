namespace MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract
{
    public interface IEntityFilterModel<TEntity, TId>
        where TEntity : Entities.Contract.IEntity<TId>
    {
        System.Linq.Expressions.Expression<System.Func<TEntity, bool>> GetSelector();
    }
}
