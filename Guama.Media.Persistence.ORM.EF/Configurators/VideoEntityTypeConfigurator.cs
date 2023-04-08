namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Configurators
{
    public class VideoEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.Video>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Video> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Thumbnail).WithMany().HasForeignKey(e => e.ThumbnailId);
        }
    }
}
