namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories
{
    public class PermissionRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Permission, int>
    {
        public PermissionRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
