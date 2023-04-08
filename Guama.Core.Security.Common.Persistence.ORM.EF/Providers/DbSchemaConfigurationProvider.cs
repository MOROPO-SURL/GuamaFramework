namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Providers
{
    public class DbSchemaConfigurationProvider : Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => "guamaframework_core_security_common";

        public System.Collections.Generic.ICollection<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator>
                {
                    new Configurators.PermissionEntityTypeConfigurator(),
                    new Configurators.RoleEntityTypeConfigurator(),
                    new Configurators.RolePermissionBridgeEntityTypeConfigurator()
                };
            }
        }
    }
}
