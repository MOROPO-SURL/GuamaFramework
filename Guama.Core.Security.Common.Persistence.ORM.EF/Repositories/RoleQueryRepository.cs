using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories;

public class RoleQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Role, System.Guid>
{
    public RoleQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Role> GetQueryableDbSet()
    {
        return baseQueryableDbSet
            .Include(entity => entity.PermissionBridges).ThenInclude(entity => entity.Stern);
    }
}