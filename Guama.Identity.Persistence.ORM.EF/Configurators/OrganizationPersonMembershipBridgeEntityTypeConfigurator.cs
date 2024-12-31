namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationPersonMembershipBridgeEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<Entities.OrganizationPersonMembershipBridge, Entities.OrganizationIdentity, System.Guid, Entities.PersonIdentity, System.Guid>
    {
        public OrganizationPersonMembershipBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PersonMembershipBridges;
            sternNavigationExpression = entity => entity.OrganizationMembershipBridges;
        }
    }
}
