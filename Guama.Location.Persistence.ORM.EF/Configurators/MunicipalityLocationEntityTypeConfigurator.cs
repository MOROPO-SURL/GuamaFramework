namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators
{
    public class MunicipalityLocationEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<Entities.MunicipalityLocation>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.MunicipalityLocation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Municipality).WithMany().HasForeignKey(e => e.MunicipalityId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.SetNull);
        }
    }
}
