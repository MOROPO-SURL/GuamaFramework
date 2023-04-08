namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PersonEmailContactItemBridge : Core.Persistence.Entities.Contract.IBridge<PersonIdentity, System.Guid, EmailContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public PersonIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public EmailContactItem Stern { get; set; }
    }
}
