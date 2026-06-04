using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class LocationQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Location, System.Guid>
{
    public LocationQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.Location> GetQueryableDbSet()
    {
        var dbset = base.GetQueryableDbSet();

        var result = new EnumerableQuery<Entities.Location>(dbset.Cast<Entities.StateLocation>().Include(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country));

        result = new EnumerableQuery<Entities.Location>(result.Concat(dbset.Cast<Entities.MunicipalityLocation>().Include(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country)));

        result = new EnumerableQuery<Entities.Location>(result.Concat(dbset.Cast<Entities.LocalityLocation>().Include(e => e.Locality).ThenInclude(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country)));

        return result;
    }
}
