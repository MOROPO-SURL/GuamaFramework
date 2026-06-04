using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories;

public class RoleCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Role, System.Guid>
{
    public RoleCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Role> GetQueryableDbSet()
    {
        return GetQueryableDbSet()
            .Include(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern);
    }
}