using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class LocalityLocationRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.LocalityLocation, System.Guid>
    {
        public LocalityLocationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.LocalityLocation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Locality).ThenInclude(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}
