using System.Collections.Generic;

namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class OrganizationIdentity : Contract.AbstractIdentity
    {
        public OrganizationIdentity()
        {
            PersonMembershipBridges = new List<OrganizationPersonMembershipBridge>();
            EmailContactItemBridges = new List<OrganizationEmailContactItemBridge>();
            PhoneContactItemBridges = new List<OrganizationPhoneContactItemBridge>();
            PostalContactItemBridges = new List<OrganizationPostalContactItemBridge>();
            SocialContactItemBridges = new List<OrganizationSocialContactItemBridge>();
            WebSiteContactItemBridges = new List<OrganizationWebSiteContactItemBridge>();
        }

        public OrganizationIdentity Owner { get; set; }
        public System.Guid? OwnerId { get; set; }

        public List<OrganizationPersonMembershipBridge> PersonMembershipBridges { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ImageId")]
        public Media.Persistence.Entities.Image Image { get; set; }
        public System.Guid? ImageId { get; set; }

        public List<OrganizationEmailContactItemBridge> EmailContactItemBridges { get; set; }

        public List<OrganizationPhoneContactItemBridge> PhoneContactItemBridges { get; set; }

        public List<OrganizationPostalContactItemBridge> PostalContactItemBridges { get; set; }

        public List<OrganizationSocialContactItemBridge> SocialContactItemBridges { get; set; }

        public List<OrganizationWebSiteContactItemBridge> WebSiteContactItemBridges { get; set; }
    }
}
