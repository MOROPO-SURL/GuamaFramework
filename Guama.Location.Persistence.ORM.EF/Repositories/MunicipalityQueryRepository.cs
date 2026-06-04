using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class MunicipalityQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Municipality, System.Guid>
{
    public MunicipalityQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Municipality> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
    }
}