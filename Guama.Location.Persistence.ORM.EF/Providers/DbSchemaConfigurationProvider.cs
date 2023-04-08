using System.Collections.Generic;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Configurators;
using MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Providers
{
    public class DbSchemaConfigurationProvider : Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public virtual string DefaultPersistenceSchema => string.Format("{0}_location", Core.Persistence.Constants.Schema.FRAMEWORK_SCHEMA_PREFIX);

        public ICollection<IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new List<IEntityTypeConfigurator>
                {
                    new CountryEntityTypeConfigurator(),
                    new StateEntityTypeConfigurator(),
                    new MunicipalityEntityTypeConfigurator(),
                    new LocalityEntityTypeConfigurator(),
                    new LocationEntityTypeConfigurator(),
                    new StateLocationEntityTypeConfigurator(),
                    new MunicipalityLocationEntityTypeConfigurator(),
                    new LocalityLocationEntityTypeConfigurator(),
                    new RegionEntityTypeConfigurator()
                };
            }
        }
    }
}
