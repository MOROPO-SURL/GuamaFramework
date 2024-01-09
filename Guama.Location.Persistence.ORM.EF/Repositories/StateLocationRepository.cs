using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class StateLocationRepository : AbstractEntityRepository<Entities.StateLocation, System.Guid>
    {
        public StateLocationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.StateLocation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}
