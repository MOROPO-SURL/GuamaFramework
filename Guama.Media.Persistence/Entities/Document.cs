namespace MOROPO.Framework.Guama.Media.Persistence.Entities
{
    public class Document : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public Document()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ContentType { get; set; }

        public long Length { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ThumbnailId")]
        public Thumbnail Thumbnail { get; set; }
        public System.Guid? ThumbnailId { get; set; }
    }
}
