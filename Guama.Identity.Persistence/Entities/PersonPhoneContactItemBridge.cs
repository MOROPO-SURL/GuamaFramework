namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PersonPhoneContactItemBridge : Core.Persistence.Entities.Contract.IBridge<PersonIdentity, System.Guid, PhoneContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public PersonIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public PhoneContactItem Stern { get; set; }
    }
}
