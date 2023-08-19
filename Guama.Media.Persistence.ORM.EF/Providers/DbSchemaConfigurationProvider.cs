namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Providers
{
    public class DbSchemaConfigurationProvider : Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => string.Format("{0}_media", Core.Persistence.Constants.Schema.FRAMEWORK_SCHEMA_PREFIX);

        public System.Collections.Generic.ICollection<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator>
                {
                    new Configurators.ImageEntityTypeConfigurator(),
                    new Configurators.ThumbnailEntityTypeConfigurator(),
                    new Configurators.VideoEntityTypeConfigurator(),
                    new Configurators.DocumentEntityTypeConfigurator()
                };
            }
        }
    }
}
