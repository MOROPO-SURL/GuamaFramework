using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract
{
    public abstract class AbstractEntityTypeConfigurator<TEntity> : IEntityTypeConfigurator
        where TEntity : class
    {
        private string tableSchema;

        public void Configure(ModelBuilder builder, string tableSchema = null)
        {
            this.tableSchema = tableSchema;

            ApplayRules(builder.Entity<TEntity>());
        }

        protected virtual void ApplayRules(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            if (!string.IsNullOrEmpty(tableSchema) && !string.IsNullOrWhiteSpace(tableSchema))
            {
                entityTypeBuilder.ToTable(entityTypeBuilder.Metadata.GetTableName(), tableSchema);
            }
        }
    }
}
