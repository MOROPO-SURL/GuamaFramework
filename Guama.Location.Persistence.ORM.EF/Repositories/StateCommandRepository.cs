using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class StateCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.State, System.Guid>
{
    public StateCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.State> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.Region).ThenInclude(e => e.Country);
    }
}
