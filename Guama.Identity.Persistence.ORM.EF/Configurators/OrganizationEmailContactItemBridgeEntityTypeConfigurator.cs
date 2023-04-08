namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationEmailContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.OrganizationEmailContactItemBridge, Entities.OrganizationIdentity, System.Guid, Entities.EmailContactItem, System.Guid>
    {
        public OrganizationEmailContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.EmailContactItemBridges;
        }
    }
}
