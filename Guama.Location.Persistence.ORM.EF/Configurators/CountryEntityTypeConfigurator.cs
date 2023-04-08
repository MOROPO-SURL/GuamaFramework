namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Configurators
{
    public class CountryEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.Country>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Country> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasIndex(entity => entity.Name).IsUnique();
            entityTypeBuilder.HasIndex(entity => entity.IsoCode).IsUnique();
        }
    }
}
