namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories;

public class PermissionCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Permission, int>
{
    public PermissionCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
