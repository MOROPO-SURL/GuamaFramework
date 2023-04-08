using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class WebSiteContactItem : Contract.AbstractContactItem
    {
        [Required, Url]
        public string Url { get; set; }
    }
}
