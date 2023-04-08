namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationPhoneContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.OrganizationPhoneContactItemBridge, Entities.OrganizationIdentity, System.Guid, Entities.PhoneContactItem, System.Guid>
    {
        public OrganizationPhoneContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PhoneContactItemBridges;
        }
    }
}
