using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class RegionCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Region, System.Guid>
{
    public RegionCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Region> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(e => e.Country);
    }
}
