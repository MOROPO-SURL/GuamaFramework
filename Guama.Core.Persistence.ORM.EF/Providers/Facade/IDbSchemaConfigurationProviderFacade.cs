namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade
{
    public interface IDbSchemaConfigurationProviderFacade
    {
        string DefaultPersistenceSchema { get; }

        System.Collections.Generic.ICollection<Configurators.Contract.IEntityTypeConfigurator> EntityTypeConfigurators { get; }
    }
}
