using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Contract
{
    public interface IModulePersistenceDbContext
    {
        DbSet<Entities.Permission> Permissions { get; set; }

        DbSet<Entities.Role> Roles { get; set; }

        DbSet<Entities.RolePermissionBridge> RolePermissionBridges { get; set; }
    }
}
