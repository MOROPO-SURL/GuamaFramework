namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Configurators
{
    public class DocumentEntityTypeConfigurator : Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.Document>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Document> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Thumbnail).WithMany().HasForeignKey(e => e.ThumbnailId);
        }
    }
}
