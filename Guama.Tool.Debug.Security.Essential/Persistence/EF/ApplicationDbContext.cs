using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities;
using MOROPO.Framework.Guama.Security.Essential.Persistence.Entities;

namespace MOROPO.Framework.Guama.Tool.Debug.Security.Essential.Persistence.EF
{
    public class ApplicationDbContext :
        Core.Persistence.ORM.EF.Context.Contract.AbstractTraceableDbContext,
        Core.Security.Common.Persistence.ORM.EF.Contract.IModulePersistenceDbContext,
        Guama.Security.Essential.Persistence.ORM.EF.Contract.IPersistenceDbContext
    {
        public ApplicationDbContext(System.Collections.Generic.IEnumerable<Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(dbSchemaConfigurationProviders, options)
        {
        }

        //#if DEBUG
        //        public override bool TraceChanges => false;
        //#endif

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermissionBridge> RolePermissionBridges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleBridge> UserRoleBridges { get; set; }
    }
}
