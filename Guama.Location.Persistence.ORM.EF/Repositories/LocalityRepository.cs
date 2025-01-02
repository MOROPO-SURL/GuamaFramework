using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class LocalityRepository : AbstractEntityRepository<Entities.Locality, System.Guid>
    {
        public LocalityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Locality> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}
