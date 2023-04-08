using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class PhoneContactItem : Contract.AbstractContactItem
    {
        [Required, Phone]
        public string PhoneNumber { get; set; }
    }
}
