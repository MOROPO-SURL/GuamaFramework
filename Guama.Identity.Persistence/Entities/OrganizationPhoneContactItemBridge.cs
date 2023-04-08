namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationPhoneContactItemBridge : Core.Persistence.Entities.Contract.IBridge<OrganizationIdentity, System.Guid, PhoneContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public OrganizationIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public PhoneContactItem Stern { get; set; }
    }
}
