using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Security.Personal.Persistence.Entities
{
    public class User : Core.Persistence.Entities.Contract.IHabilitableEntity<string>, Core.Persistence.Entities.Contract.IPersistenceSignable
    {
        [Key]
        public string Id { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public string Password { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public bool HasFullAccess { get; set; }

        [Required]
        public System.DateTime Created { get; private set; }

        public System.DateTime LastSessionDate { get; set; }

        public int FailAuthenticationCount { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public Identity.Persistence.Entities.PersonIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        public System.Collections.Generic.ICollection<UserRoleBridge> RoleBridges { get; set; }

        [Required]
        public string Signature { get; set; }

        public User()
        {
            Created = System.DateTime.Now;
            Identity = new Identity.Persistence.Entities.PersonIdentity();
            RoleBridges = new System.Collections.Generic.List<UserRoleBridge>();
        }

        public object GetSignableEntityModel()
        {
            return new { Id, Enabled, HasFullAccess, Password };
        }
    }
}
