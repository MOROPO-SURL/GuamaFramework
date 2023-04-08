using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class StateRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.State, System.Guid>
    {
        public StateRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.State, object> GetPagingOrderSelector()
        {
            return e => e.Name;
        }

        protected override System.Linq.IQueryable<Entities.State> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}
