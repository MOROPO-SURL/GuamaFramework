namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationPostalContactItemBridge : Core.Persistence.Entities.Contract.IBridge<OrganizationIdentity, System.Guid, PostalContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public OrganizationIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public PostalContactItem Stern { get; set; }
    }
}
