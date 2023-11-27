namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Providers
{
    public class DbSchemaConfigurationProvider : Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => string.Format("{0}_security_personal", Core.Persistence.Constants.Schema.FRAMEWORK_SCHEMA_PREFIX);

        public System.Collections.Generic.ICollection<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator>
                {
                    new Configurators.UserEntityTypeConfigurator(),
                    new Configurators.UserRoleBridgeEntityTypeConfigurator()
                };
            }
        }
    }
}
