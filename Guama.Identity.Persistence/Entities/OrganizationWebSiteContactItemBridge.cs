namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationWebSiteContactItemBridge : Core.Persistence.Entities.Contract.IBridge<OrganizationIdentity, System.Guid, WebSiteContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public OrganizationIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public WebSiteContactItem Stern { get; set; }
    }
}
