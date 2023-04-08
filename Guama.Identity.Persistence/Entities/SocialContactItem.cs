namespace MOROPO.Framework.Guama.Identity.Persistence.Entities
{
    public class SocialContactItem : Contract.AbstractContactItem
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string SocialId { get; set; }
    }
}
