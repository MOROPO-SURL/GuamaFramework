using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Media.Persistence.Entities
{
    public class Video : Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public Video()
        {
            Id = System.Guid.NewGuid();
            CreateAt = System.DateTime.Now;
        }

        public System.Guid Id { get; set; }

        public System.DateTime CreateAt { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public long Length { get; set; }

        [MaxLength(255)]
        public string About { get; set; }

        [Required]
        public System.TimeSpan Duration { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string FrameSize { get; set; }

        [Required]
        public string ColorModel { get; set; }

        [Required]
        public double Fps { get; set; }

        [Required]
        public string AudioFormat { get; set; }

        [Required]
        public string AudioChannelOutput { get; set; }

        public Thumbnail Thumbnail { get; set; }

        public System.Guid? ThumbnailId { get; set; }
    }
}
