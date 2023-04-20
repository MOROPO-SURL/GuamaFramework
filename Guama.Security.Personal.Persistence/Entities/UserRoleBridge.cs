namespace MOROPO.Framework.Guama.Security.Personal.Persistence.Entities
{
    public class UserRoleBridge :
        Core.Persistence.Entities.Contract.IBridge<User, string, Core.Security.Common.Persistence.Entities.Role, System.Guid>,
        Core.Persistence.Entities.Contract.IPersistenceSignable
    {
        
        public string ProwId { get; set; }
        public User Prow { get; set; }
        
        public System.Guid SternId { get; set; }
        public Core.Security.Common.Persistence.Entities.Role Stern { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Signature { get; set; }

        public object GetSignableEntityModel()
        {
            return new { ProwId, SternId };
        }
    }
}
