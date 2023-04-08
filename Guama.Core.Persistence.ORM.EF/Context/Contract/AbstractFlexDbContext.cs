using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Context.Contract
{
    public abstract class AbstractFlexDbContext : DbContext
    {
        private readonly System.Collections.Generic.IEnumerable<IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders;

        public AbstractFlexDbContext(System.Collections.Generic.IEnumerable<IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(options)
        {
            this.dbSchemaConfigurationProviders = dbSchemaConfigurationProviders;
        }

        public virtual bool IsCrossPlatformCompatible { get => true; }

        public virtual bool AllowTableSchemaUsage { get => true; }

        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var dbSchemaConfigurationProvider in dbSchemaConfigurationProviders)
            {
                foreach (var configurator in dbSchemaConfigurationProvider.EntityTypeConfigurators)
                {
                    if (string.IsNullOrEmpty(dbSchemaConfigurationProvider.DefaultPersistenceSchema) || string.IsNullOrWhiteSpace(dbSchemaConfigurationProvider.DefaultPersistenceSchema))
                    {
                        configurator.Configure(modelBuilder);
                    }
                    else
                    {
                        configurator.Configure(modelBuilder, dbSchemaConfigurationProvider.DefaultPersistenceSchema);
                    }
                }
            }

            OnFlexModelCreating(modelBuilder);

            if (!IsCrossPlatformCompatible) return;

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToLower());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToLower());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToLower());
                }
            }
        }

        protected virtual void OnFlexModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
