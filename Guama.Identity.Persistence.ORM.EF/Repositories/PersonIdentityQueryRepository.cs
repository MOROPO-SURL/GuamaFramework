using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class PersonIdentityQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.PersonIdentity, System.Guid>
{
    public PersonIdentityQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.PersonIdentity> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.Image).ThenInclude(e => e.Thumbnail)
            .Include(e => e.OrganizationMembershipBridges).ThenInclude(b => b.Prow).ThenInclude(e => e.Image).ThenInclude(e => e.Thumbnail);
    }
}
