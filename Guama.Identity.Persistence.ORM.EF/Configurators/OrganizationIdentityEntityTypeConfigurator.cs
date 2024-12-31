namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Configurators
{
    public class OrganizationIdentityEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<Entities.OrganizationIdentity>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.OrganizationIdentity> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Owner).WithMany().HasForeignKey(e => e.OwnerId);
        }
    }
}
