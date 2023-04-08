namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Configurators
{
    public class RolePermissionBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.RolePermissionBridge, Entities.Role, System.Guid, Entities.Permission, int>
    {
        public RolePermissionBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PermissionBridges;
            sternNavigationExpression = entity => entity.RoleBridges;
        }
    }
}
