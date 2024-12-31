namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationPostalContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<Entities.OrganizationPostalContactItemBridge, Entities.OrganizationIdentity, System.Guid, Entities.PostalContactItem, System.Guid>
    {
        public OrganizationPostalContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PostalContactItemBridges;
        }
    }
}
