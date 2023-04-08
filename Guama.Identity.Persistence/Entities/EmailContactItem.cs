using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class EmailContactItem : Contract.AbstractContactItem
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
    }
}
