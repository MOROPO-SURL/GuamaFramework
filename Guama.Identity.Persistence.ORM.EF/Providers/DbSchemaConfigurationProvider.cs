using MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Providers
{
    public class DbSchemaConfigurationProvider : Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public virtual string DefaultPersistenceSchema => string.Format("{0}_identity", Core.Persistence.Constants.Schema.FRAMEWORK_SCHEMA_PREFIX);

        public System.Collections.Generic.ICollection<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator>
                {
                    new EmailContactItemEntityTypeConfigurator(),
                    new PhoneContactItemEntityTypeConfigurator(),
                    new SocialContactItemEntityTypeConfigurator(),
                    new WebSiteContactItemEntityTypeConfigurator(),
                    new PostalContactItemEntityTypeConfigurator(),
                    new PersonIdentityEntityTypeConfigurator(),
                    new OrganizationIdentityEntityTypeConfigurator(),
                    new OrganizationPersonMembershipBridgeEntityTypeConfigurator(),
                    new PersonEmailContactItemBridgeEntityTypeConfigurator(),
                    new PersonPhoneContactItemBridgeEntityTypeConfigurator(),
                    new PersonPostalContactItemBridgeEntityTypeConfigurator(),
                    new PersonSocialContactItemBridgeEntityTypeConfigurator(),
                    new PersonWebSiteContactItemBridgeEntityTypeConfigurator(),
                    new OrganizationEmailContactItemBridgeEntityTypeConfigurator(),
                    new OrganizationPhoneContactItemBridgeEntityTypeConfigurator(),
                    new OrganizationPostalContactItemBridgeEntityTypeConfigurator(),
                    new OrganizationSocialContactItemBridgeEntityTypeConfigurator(),
                    new OrganizationWebSiteContactItemBridgeEntityTypeConfigurator()
                };
            }
        }
    }
}
