using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class LocalityLocationCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.LocalityLocation, System.Guid>
{
    public LocalityLocationCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.LocalityLocation> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(e => e.Locality).ThenInclude(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
    }
}
