namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Providers
{
    public sealed class RoleMemoryProvider : Core.Presentation.Web.Security.Providers.Facade.IRoleIdProviderFacade
    {
        private static RoleMemoryProvider instance;

        public static RoleMemoryProvider Instance
        {
            get { return instance ?? (instance = new RoleMemoryProvider()); }
        }

        private RoleMemoryProvider()
        {
            Roles = new System.Collections.Generic.List<Persistence.Entities.Role>();
        }

        public System.Collections.Generic.List<Persistence.Entities.Role> Roles { get; private set; }

        public System.Guid[] GetByPermission(string permission)
        {
            var roleIds = new System.Collections.Generic.List<System.Guid>();

            foreach (var role in Roles)
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
