namespace MOROPO.Framework.Guama.Security.Essential.Persistence.ORM.EF.Configurators
{
    public class UserRoleBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.UserRoleBridge, Entities.User, string, Core.Security.Common.Persistence.Entities.Role, System.Guid>
    {
        public UserRoleBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.RoleBridges;
        }
    }
}
