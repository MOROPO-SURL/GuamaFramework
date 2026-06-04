namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories;

public class PermissionQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Permission, int>
{
    public PermissionQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
