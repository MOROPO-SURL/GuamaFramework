namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators
{
    public class LocalityEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<Entities.Locality>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Locality> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasIndex(entity => new { entity.Name, entity.MunicipalityId }).IsUnique();
        }
    }
}
