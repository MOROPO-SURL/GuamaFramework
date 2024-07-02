using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Managers
{
    public sealed class RoleMemoryManager : IRoleMemoryManagerFacade
    {
        private readonly System.Collections.Generic.List<Role> roles;

        public RoleMemoryManager()
        {
            roles = new System.Collections.Generic.List<Role>();
        }

        public void RememberRole(Role role)
        {
            roles.Add(role);
        }
        public Role FindRoleById(System.Guid roleId)
        {
            return roles.First(item => item.Id == roleId);
        }

        public void ForguetRoleById(System.Guid roleId)
        {
            var target = roles.First(item => item.Id == roleId);

            roles.Remove(target);
        }

        public System.Guid[] GetRoleIdsByPermission(string permission)
        {
            var roleIds = new System.Collections.Generic.List<System.Guid>();

            foreach (var role in roles)
            {
                foreach (var bridge in role.PermissionBridges)
                {
                    if (bridge.Stern.Code == permission)
                    {
                        roleIds.Add(role.Id);

                        break;
                    }
                }
            };

            return roleIds.ToArray();
        }
    }
}
