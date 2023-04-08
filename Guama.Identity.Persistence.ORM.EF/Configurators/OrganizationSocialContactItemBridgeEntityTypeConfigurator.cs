namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationSocialContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.OrganizationSocialContactItemBridge, Entities.OrganizationIdentity, System.Guid, Entities.SocialContactItem, System.Guid>
    {
        public OrganizationSocialContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.SocialContactItemBridges;
        }
    }
}
