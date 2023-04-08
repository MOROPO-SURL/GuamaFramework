namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class PersonPostalContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PersonPostalContactItemBridge, Entities.PersonIdentity, System.Guid, Entities.PostalContactItem, System.Guid>
    {
        public PersonPostalContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PostalContactItemBridges;
        }
    }
}
