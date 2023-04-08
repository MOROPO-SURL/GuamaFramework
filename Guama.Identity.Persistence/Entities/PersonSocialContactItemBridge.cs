namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PersonSocialContactItemBridge : Core.Persistence.Entities.Contract.IBridge<PersonIdentity, System.Guid, SocialContactItem, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public PersonIdentity Prow { get; set; }

        public System.Guid SternId { get; set; }
        public SocialContactItem Stern { get; set; }
    }
}
