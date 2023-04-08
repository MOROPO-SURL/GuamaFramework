using System.Linq;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Extensions;
using MOROPO.Framework.Guama.Core.Security.Presentation.Constants;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Helpers
{
    public class DataInitializationHelper : Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(Microsoft.EntityFrameworkCore.DbContext context)
        {
            var permissionDbSet = context.Set<Persistence.Entities.Permission>();

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.LicensingManager)))
            {
                permissionDbSet.Add(Permissions.LicensingManager.ToPermissionEntity());
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.ShellManager)))
            {
                permissionDbSet.Add(Permissions.ShellManager.ToPermissionEntity());
            }

            // User Permissions definition!!
            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.UserViewer)))
            {
                permissionDbSet.Add(Permissions.UserViewer.ToPermissionEntity(PermissionFamilies.UserManagement));
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.UserCreator)))
            {
                permissionDbSet.Add(Permissions.UserCreator.ToPermissionEntity(PermissionFamilies.UserManagement));
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.UserEditor)))
            {
                permissionDbSet.Add(Permissions.UserEditor.ToPermissionEntity(PermissionFamilies.UserManagement));
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.UserRemover)))
            {
                permissionDbSet.Add(Permissions.UserRemover.ToPermissionEntity(PermissionFamilies.UserManagement));
            }

            // Permission Permissions definition!!
            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.RoleViewer)))
            {
                permissionDbSet.Add(Permissions.RoleViewer.ToPermissionEntity(PermissionFamilies.RoleManagement));
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.RoleCreator)))
            {
                permissionDbSet.Add(Permissions.RoleCreator.ToPermissionEntity(PermissionFamilies.RoleManagement));
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.RoleEditor)))
            {
                permissionDbSet.Add(Permissions.RoleEditor.ToPermissionEntity(PermissionFamilies.RoleManagement));
            }

            if (!permissionDbSet.Any(entity => entity.Code.Equals(Permissions.RoleRemover)))
            {
                permissionDbSet.Add(Permissions.RoleRemover.ToPermissionEntity(PermissionFamilies.RoleManagement));
            }

            if (context is Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext)
            {
                (context as Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                context.SaveChanges();
            }
        }
    }
}
