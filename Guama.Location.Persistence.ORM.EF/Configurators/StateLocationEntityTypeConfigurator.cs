namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators
{
    public class StateLocationEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.StateLocation>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.StateLocation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.State).WithMany().HasForeignKey(e => e.StateId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.SetNull);
        }
    }
}
