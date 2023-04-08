namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationSocialContactItemBridge : Core.Persistence.Entities.Contract.IBridge<OrganizationIdentity, System.Guid, SocialContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public OrganizationIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public SocialContactItem Stern { get; set; }
    }
}
