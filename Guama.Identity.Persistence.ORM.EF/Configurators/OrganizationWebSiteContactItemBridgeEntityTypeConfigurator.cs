namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationWebSiteContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.OrganizationWebSiteContactItemBridge, Entities.OrganizationIdentity, System.Guid, Entities.WebSiteContactItem, System.Guid>
    {
        public OrganizationWebSiteContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.WebSiteContactItemBridges;
        }
    }
}