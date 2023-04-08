using System.Collections.Generic;

namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PersonIdentity : Contract.AbstractIdentity
    {
        public PersonIdentity()
        {
            OrganizationMembershipBridges = new List<OrganizationPersonMembershipBridge>();
            EmailContactItemBridges = new List<PersonEmailContactItemBridge>();
            PhoneContactItemBridges = new List<PersonPhoneContactItemBridge>();
            PostalContactItemBridges = new List<PersonPostalContactItemBridge>();
            SocialContactItemBridges = new List<PersonSocialContactItemBridge>();
            WebSiteContactItemBridges = new List<PersonWebSiteContactItemBridge>();
        }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public Enumerations.Sex Sex { get; set; }

        public Enumerations.CivilStatus CivilStatus { get; set; }

        public ICollection<OrganizationPersonMembershipBridge> OrganizationMembershipBridges { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ImageId")]
        public Media.Persistence.Entities.Image Image { get; set; }
        public System.Guid? ImageId { get; set; }

        public List<PersonEmailContactItemBridge> EmailContactItemBridges { get; set; }

        public List<PersonPhoneContactItemBridge> PhoneContactItemBridges { get; set; }

        public List<PersonPostalContactItemBridge> PostalContactItemBridges { get; set; }

        public List<PersonSocialContactItemBridge> SocialContactItemBridges { get; set; }

        public List<PersonWebSiteContactItemBridge> WebSiteContactItemBridges { get; set; }
    }
}
