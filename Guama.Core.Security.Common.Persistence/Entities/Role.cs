namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities
{
    public class Role : Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public Role()
        {
            Id = System.Guid.NewGuid();
            PermissionBridges = new System.Collections.Generic.List<RolePermissionBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public System.Collections.Generic.ICollection<RolePermissionBridge> PermissionBridges { get; set; }
    }
}
