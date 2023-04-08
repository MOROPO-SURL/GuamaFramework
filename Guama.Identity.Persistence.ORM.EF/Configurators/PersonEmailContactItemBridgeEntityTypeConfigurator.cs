namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class PersonEmailContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PersonEmailContactItemBridge, Entities.PersonIdentity, System.Guid, Entities.EmailContactItem, System.Guid>
    {
        public PersonEmailContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.EmailContactItemBridges;
        }
    }
}
