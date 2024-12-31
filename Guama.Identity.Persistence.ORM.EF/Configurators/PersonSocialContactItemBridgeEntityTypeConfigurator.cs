namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class PersonSocialContactItemBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<Entities.PersonSocialContactItemBridge, Entities.PersonIdentity, System.Guid, Entities.SocialContactItem, System.Guid>
    {
        public PersonSocialContactItemBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.SocialContactItemBridges;
        }
    }
}
