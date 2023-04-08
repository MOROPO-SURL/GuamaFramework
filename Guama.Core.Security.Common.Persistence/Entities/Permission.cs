namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities
{
    public class Permission : Core.Persistence.Entities.Contract.INamedEntity<int>
    {
        public Permission()
        {
            RoleBridges = new System.Collections.Generic.List<RolePermissionBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Code { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Family { get; set; }

        public string Description { get; set; }

        public System.Collections.Generic.ICollection<RolePermissionBridge> RoleBridges { get; set; }
    }
}
