using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class OrganizationIdentityRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.OrganizationIdentity, System.Guid>
    {
        public OrganizationIdentityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.OrganizationIdentity> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Image).ThenInclude(e => e.Thumbnail)
                .Include(e => e.PersonMembershipBridges).ThenInclude(b => b.Stern).ThenInclude(e => e.Image).ThenInclude(e => e.Thumbnail);
        }
    }
}
