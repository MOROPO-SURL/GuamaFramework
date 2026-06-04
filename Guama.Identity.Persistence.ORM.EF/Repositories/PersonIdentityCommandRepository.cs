using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class PersonIdentityCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.PersonIdentity, System.Guid>
{
    public PersonIdentityCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.PersonIdentity> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            .Include(e => e.Image).ThenInclude(e => e.Thumbnail)
            .Include(e => e.OrganizationMembershipBridges).ThenInclude(b => b.Prow).ThenInclude(e => e.Image).ThenInclude(e => e.Thumbnail);
    }
}
