namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators
{
    public class MunicipalityEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.Municipality>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Municipality> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasIndex(entity => new { entity.Name, entity.StateId }).IsUnique();
        }
    }
}
