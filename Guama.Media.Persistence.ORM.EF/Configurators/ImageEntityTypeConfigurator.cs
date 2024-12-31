namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Configurators
{
    public class ImageEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<Entities.Image>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Image> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Thumbnail).WithMany().HasForeignKey(e => e.ThumbnailId);
        }
    }
}
