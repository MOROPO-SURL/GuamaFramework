namespace MOROPO.Framework.Guama.Security.Essential.Persistence.ORM.EF.Helpers.Integrity
{
    public static class UserRoleBridgeIntegrityHelper
    {
        public static string GetSignature(Entities.UserRoleBridge entity)
        {
            return Core.Persistence.Helpers.DataIntegrityHelper.GetEntitySignature(entity);
        }

        public static bool IsCorrectlySigned(Entities.UserRoleBridge entity)
        {
            return entity.Signature == GetSignature(entity);
        }
    }
}
