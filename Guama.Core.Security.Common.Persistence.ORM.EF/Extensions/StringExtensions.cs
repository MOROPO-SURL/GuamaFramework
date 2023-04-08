namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Extensions
{
    public static class StringExtensions
    {
        public static Entities.Permission ToPermissionEntity(this string code, string family = null)
        {
            return new Entities.Permission
            {
                Name = code,
                Code = code,
                Family = family,
                Description = string.Format("{0}-description", code)
            };
        }
    }
}
