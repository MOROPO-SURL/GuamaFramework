using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Repositories
{
    public class PermissionRepository : AbstractEntityRepository<Entities.Permission, int>
    {
        public PermissionRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
