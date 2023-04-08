using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Media.Persistence.Entities
{
    public class Thumbnail : Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public Thumbnail()
        {
            Id = System.Guid.NewGuid();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public long Length { get; set; }

        [MaxLength(255)]
        public string About { get; set; }
    }
}
