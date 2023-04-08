namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationPersonMembershipBridge : Core.Persistence.Entities.Contract.IBridge<OrganizationIdentity, System.Guid, PersonIdentity, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public OrganizationIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public PersonIdentity Stern { get; set; }

        public string Role { get; set; }
    }
}
