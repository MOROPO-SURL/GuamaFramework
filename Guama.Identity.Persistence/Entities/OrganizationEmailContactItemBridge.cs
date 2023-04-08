namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationEmailContactItemBridge : Core.Persistence.Entities.Contract.IBridge<OrganizationIdentity, System.Guid, EmailContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public OrganizationIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public EmailContactItem Stern { get; set; }
    }
}
