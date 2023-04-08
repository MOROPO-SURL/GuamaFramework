namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class PersonPhoneContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PersonPhoneContactItemBridge, Entities.PersonIdentity, System.Guid, Entities.PhoneContactItem, System.Guid>
    {
        public PersonPhoneContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PhoneContactItemBridges;
        }
    }
}
