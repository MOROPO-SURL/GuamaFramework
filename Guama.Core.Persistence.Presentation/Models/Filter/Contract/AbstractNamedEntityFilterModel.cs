namespace MOROPO.Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract
{
    public abstract class AbstractNamedEntityFilterModel<TNamedEntity, TId> : IEntityFilterModel<TNamedEntity, TId>
        where TNamedEntity : Entities.Contract.INamedEntity<TId>
    {
        public string Name { get; set; }

        public System.Linq.Expressions.Expression<System.Func<TNamedEntity, bool>> GetSelector()
        {
            if (Name == null || Name == string.Empty) return null;

            return entity => entity.Name.Contains(Name);
        }
    }
}
