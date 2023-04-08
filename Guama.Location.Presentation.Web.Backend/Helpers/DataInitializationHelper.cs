using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Extensions;
using MOROPO.Framework.Guama.Location.Presentation.Constants;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Helpers
{
    public class DataInitializationHelper : Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(DbContext context)
        {
            var permissionDbSet = context.Set<Core.Security.Common.Persistence.Entities.Permission>();

            // Country Permissions definition!!
            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.CountryViewer)).Result)
            {
                permissionDbSet.Add(Permissions.CountryViewer.ToPermissionEntity(PermissionFamilies.CountryManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.CountryCreator)).Result)
            {
                permissionDbSet.Add(Permissions.CountryCreator.ToPermissionEntity(PermissionFamilies.CountryManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.CountryEditor)).Result)
            {
                permissionDbSet.Add(Permissions.CountryEditor.ToPermissionEntity(PermissionFamilies.CountryManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.CountryRemover)).Result)
            {
                permissionDbSet.Add(Permissions.CountryRemover.ToPermissionEntity(PermissionFamilies.CountryManagement));
            }

            // Region Permissions definition!!
            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.RegionViewer)).Result)
            {
                permissionDbSet.Add(Permissions.RegionViewer.ToPermissionEntity(PermissionFamilies.RegionManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.RegionCreator)).Result)
            {
                permissionDbSet.Add(Permissions.RegionCreator.ToPermissionEntity(PermissionFamilies.RegionManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.RegionEditor)).Result)
            {
                permissionDbSet.Add(Permissions.RegionEditor.ToPermissionEntity(PermissionFamilies.RegionManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.RegionRemover)).Result)
            {
                permissionDbSet.Add(Permissions.RegionRemover.ToPermissionEntity(PermissionFamilies.RegionManagement));
            }

            // State Permissions definition!!
            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.StateViewer)).Result)
            {
                permissionDbSet.Add(Permissions.StateViewer.ToPermissionEntity(PermissionFamilies.StateManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.StateCreator)).Result)
            {
                permissionDbSet.Add(Permissions.StateCreator.ToPermissionEntity(PermissionFamilies.StateManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.StateEditor)).Result)
            {
                permissionDbSet.Add(Permissions.StateEditor.ToPermissionEntity(PermissionFamilies.StateManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.StateRemover)).Result)
            {
                permissionDbSet.Add(Permissions.StateRemover.ToPermissionEntity(PermissionFamilies.StateManagement));
            }

            // Municipality Permissions definition!!
            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.MunicipalityViewer)).Result)
            {
                permissionDbSet.Add(Permissions.MunicipalityViewer.ToPermissionEntity(PermissionFamilies.MunicipalityManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.MunicipalityCreator)).Result)
            {
                permissionDbSet.Add(Permissions.MunicipalityCreator.ToPermissionEntity(PermissionFamilies.MunicipalityManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.MunicipalityEditor)).Result)
            {
                permissionDbSet.Add(Permissions.MunicipalityEditor.ToPermissionEntity(PermissionFamilies.MunicipalityManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.MunicipalityRemover)).Result)
            {
                permissionDbSet.Add(Permissions.MunicipalityRemover.ToPermissionEntity(PermissionFamilies.MunicipalityManagement));
            }

            // Locality Permissions definition!!
            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.LocalityViewer)).Result)
            {
                permissionDbSet.Add(Permissions.LocalityViewer.ToPermissionEntity(PermissionFamilies.LocalityManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.LocalityCreator)).Result)
            {
                permissionDbSet.Add(Permissions.LocalityCreator.ToPermissionEntity(PermissionFamilies.LocalityManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.LocalityEditor)).Result)
            {
                permissionDbSet.Add(Permissions.LocalityEditor.ToPermissionEntity(PermissionFamilies.LocalityManagement));
            }

            if (!permissionDbSet.AnyAsync(item => item.Code.Equals(Permissions.LocalityRemover)).Result)
            {
                permissionDbSet.Add(Permissions.LocalityRemover.ToPermissionEntity(PermissionFamilies.LocalityManagement));
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
