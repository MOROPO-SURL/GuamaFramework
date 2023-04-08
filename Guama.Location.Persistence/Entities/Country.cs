using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class Country : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public Country()
        {
            Id = System.Guid.NewGuid();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, RegularExpression(@"^[a-z]{2}$")]
        public string IsoCode { get; set; }
    }
}
