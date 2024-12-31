namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class PostalContactItemEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<Entities.PostalContactItem>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PostalContactItem> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId);
        }
    }
}
