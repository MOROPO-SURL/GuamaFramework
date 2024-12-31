namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Configurators
{
    public class UserEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<Entities.User>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.User> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(o => o.Identity).WithOne().HasForeignKey<Entities.User>(a => a.IdentityId);
        }
    }
}
