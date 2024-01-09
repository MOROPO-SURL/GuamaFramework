using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class OrganizationIdentityRepository : AbstractEntityRepository<Entities.OrganizationIdentity, System.Guid>
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
