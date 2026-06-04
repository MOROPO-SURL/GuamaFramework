using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class MunicipalityLocationQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.MunicipalityLocation, System.Guid>
{
    public MunicipalityLocationQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.MunicipalityLocation> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
    }
}
