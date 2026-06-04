using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class LocalityCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Locality, System.Guid>
{
    public LocalityCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Locality> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
    }
}
