using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Repositories;

public class UserQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.User, string>
{
    public UserQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.User> GetQueryableDbSet()
    {
        return GetQueryableDbSet()
            .Include(entity => entity.RoleBridges).ThenInclude(entity => entity.Stern).ThenInclude(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern)
            .Include(entity => entity.Identity).ThenInclude(entity => entity.Image).ThenInclude(entity => entity.Thumbnail);
    }
}
