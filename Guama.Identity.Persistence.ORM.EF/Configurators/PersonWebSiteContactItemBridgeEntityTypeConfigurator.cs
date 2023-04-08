namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class PersonWebSiteContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PersonWebSiteContactItemBridge, Entities.PersonIdentity, System.Guid, Entities.WebSiteContactItem, System.Guid>
    {
        public PersonWebSiteContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.WebSiteContactItemBridges;
        }
    }
}
