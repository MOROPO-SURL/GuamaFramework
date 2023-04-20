namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Helpers.Integrity
{
    public static class UserIntegrityHelper
    {
        public static string GetSignature(Entities.User entity)
        {
            return Core.Persistence.Helpers.DataIntegrityHelper.GetEntitySignature(entity);
        }

        public static bool IsCorrectlySigned(Entities.User entity)
        {
            return entity.Signature == GetSignature(entity);
        }
    }
}
