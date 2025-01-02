using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class StateRepository : AbstractEntityRepository<Entities.State, System.Guid>
    {
        public StateRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.State> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}
