using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Configurators
{
    public abstract class AbstractEntityTypeConfigurator<TEntity> : IEntityTypeConfigurator
        where TEntity : class
    {
        private string schemaPrefix;
        private string tableSchema;

        public void Configure(ModelBuilder builder, string schemaPrefix = null, string tableSchema = null)
        {
            this.schemaPrefix = schemaPrefix;
            this.tableSchema = tableSchema;

            ApplayRules(builder.Entity<TEntity>());
        }

        protected virtual void ApplayRules(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            if (!string.IsNullOrEmpty(schemaPrefix) && !string.IsNullOrWhiteSpace(schemaPrefix) && !string.IsNullOrEmpty(tableSchema) && !string.IsNullOrWhiteSpace(tableSchema))
            {
                entityTypeBuilder.ToTable(entityTypeBuilder.Metadata.GetTableName(), schemaPrefix + "_" + tableSchema);
            }
        }
    }
}
