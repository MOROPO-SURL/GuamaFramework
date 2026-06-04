using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Repositories;

public class UserCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.User, string>
{
    public UserCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.User> GetQueryableDbSet()
    {
        return GetQueryableDbSet()
            .Include(entity => entity.RoleBridges).ThenInclude(entity => entity.Stern).ThenInclude(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern)
            .Include(entity => entity.Identity).ThenInclude(entity => entity.Image).ThenInclude(entity => entity.Thumbnail);
    }
}
