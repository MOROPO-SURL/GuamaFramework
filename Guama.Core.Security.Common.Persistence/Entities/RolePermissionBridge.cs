namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities
{
    public class RolePermissionBridge : Core.Persistence.Entities.Contract.IBridge<Role, System.Guid, Permission, int>
    {
        public System.Guid ProwId { get; set; }
        public Role Prow { get; set; }

        public int SternId { get; set; }
        public Permission Stern { get; set; }
    }
}