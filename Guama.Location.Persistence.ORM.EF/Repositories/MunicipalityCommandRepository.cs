using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class MunicipalityCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Municipality, System.Guid>
{
    public MunicipalityCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Municipality> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
    }
}