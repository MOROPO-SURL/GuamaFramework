using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Identity.Persistence.Entities;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Context
{
    public interface IPersistenceDbContext :
        Media.Persistence.ORM.EF.Context.IPersistenceDbContext,
        Location.Persistence.ORM.EF.Context.IPersistenceDbContext
    {
        DbSet<PersonIdentity> PersonIdentities { get; set; }

        DbSet<OrganizationIdentity> OrganizationIdentities { get; set; }

        DbSet<OrganizationPersonMembershipBridge> OrganizationPersonMembershipBridges { get; set; }

        DbSet<EmailContactItem> EmailContactItems { get; set; }

        DbSet<PhoneContactItem> PhoneContactItems { get; set; }

        DbSet<SocialContactItem> SocialContactItems { get; set; }

        DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }

        DbSet<PostalContactItem> PostalContactItems { get; set; }

        DbSet<PersonEmailContactItemBridge> PersonEmailContactItemBridges { get; set; }

        DbSet<PersonPhoneContactItemBridge> PersonPhoneContactItemBridges { get; set; }

        DbSet<PersonPostalContactItemBridge> PersonPostalContactItemBridges { get; set; }

        DbSet<PersonSocialContactItemBridge> PersonSocialContactItemBridges { get; set; }

        DbSet<PersonWebSiteContactItemBridge> PersonWebSiteContactItemBridges { get; set; }

        DbSet<OrganizationEmailContactItemBridge> OrganizationEmailContactItemBridges { get; set; }

        DbSet<OrganizationPhoneContactItemBridge> OrganizationPhoneContactItemBridges { get; set; }

        DbSet<OrganizationPostalContactItemBridge> OrganizationPostalContactItemBridges { get; set; }

        DbSet<OrganizationSocialContactItemBridge> OrganizationSocialContactItemBridges { get; set; }

        DbSet<OrganizationWebSiteContactItemBridge> OrganizationWebSiteContactItemBridges { get; set; }
    }
}
