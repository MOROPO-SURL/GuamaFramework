namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators
{
    public class LocalityLocationEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.LocalityLocation>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.LocalityLocation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Locality).WithMany().HasForeignKey(e => e.LocalityId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.SetNull);
        }
    }
}
