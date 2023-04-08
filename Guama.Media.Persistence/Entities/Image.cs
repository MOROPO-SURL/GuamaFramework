using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Media.Persistence.Entities
{
    public class Image : Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public Image()
        {
            Id = System.Guid.NewGuid();
            CreateAt = System.DateTime.Now;
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreateAt { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public long Length { get; set; }

        [MaxLength(255)]
        public string About { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        public Thumbnail Thumbnail { get; set; }

        public System.Guid? ThumbnailId { get; set; }
    }
}
