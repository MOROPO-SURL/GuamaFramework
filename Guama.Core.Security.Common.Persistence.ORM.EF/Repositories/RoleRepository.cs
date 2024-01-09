using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories
{
    public class RoleRepository : AbstractEntityRepository<Entities.Role, System.Guid>
    {
        public RoleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Role> GetQueryableDbSet()
        {
            return DbContext.Set<Entities.Role>()
                .Include(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern);
        }
    }
}