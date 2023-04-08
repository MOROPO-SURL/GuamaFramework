using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class RegionRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Region, System.Guid>
    {
        public RegionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Region, object> GetPagingOrderSelector()
        {
            return e => e.Name;
        }

        protected override System.Linq.IQueryable<Entities.Region> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Country);
        }
    }
}
