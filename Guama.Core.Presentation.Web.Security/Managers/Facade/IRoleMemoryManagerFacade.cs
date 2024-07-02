namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade
{
    public interface IRoleMemoryManagerFacade
    {
        void RememberRole(Core.Security.Common.Persistence.Entities.Role role);

        Core.Security.Common.Persistence.Entities.Role FindRoleById(System.Guid roleId);

        void ForguetRoleById(System.Guid roleId);

        System.Guid[] GetRoleIdsByPermission(string permission);
    }
}
