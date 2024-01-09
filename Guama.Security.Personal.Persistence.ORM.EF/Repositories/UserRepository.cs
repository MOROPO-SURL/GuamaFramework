using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Repositories
{
    public class UserRepository : AbstractEntityRepository<Entities.User, string>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.User> GetQueryableDbSet()
        {
            return DbContext.Set<Entities.User>()
                .Include(entity => entity.RoleBridges).ThenInclude(entity => entity.Stern).ThenInclude(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.Image).ThenInclude(entity => entity.Thumbnail);
        }
    }
}
