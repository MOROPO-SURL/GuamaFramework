using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Security.Essential.Persistence.ORM.EF.Repositories
{
    public class UserRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.User, string>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.User> GetQueryableDbSet()
        {
            return DbContext.Set<Entities.User>()
                .Include(entity => entity.RoleBridges).ThenInclude(entity => entity.Stern).ThenInclude(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern);
        }
    }
}
