using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class StateQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.State, System.Guid>
{
    public StateQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.State> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.Region).ThenInclude(e => e.Country);
    }
}
