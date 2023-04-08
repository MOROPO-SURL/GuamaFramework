namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PersonPostalContactItemBridge : Core.Persistence.Entities.Contract.IBridge<PersonIdentity, System.Guid, PostalContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public PersonIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public PostalContactItem Stern { get; set; }
    }
}
