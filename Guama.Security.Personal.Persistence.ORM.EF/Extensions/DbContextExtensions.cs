using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Contract;

namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Create a User at target DbContext and return an symetric encrypted password
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="target"></param>
        /// <param name="mode"></param>
        /// <param name="userId"></param>
        /// <param name="persistenceChangeAuthor"></param>
        /// <returns>Symetric encrypted password</returns>
        public static string CreateSecureUser<TDbContext>(this TDbContext target, System.Guid mode, string userId, string persistenceChangeAuthor = null)
            where TDbContext : Microsoft.EntityFrameworkCore.DbContext, IPersistenceDbContext
        {
            var vendoEnanitosVerdes = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();

            var a3s1V = new byte[16];

            string result;

            using (var a3s = System.Security.Cryptography.Aes.Create())
            {
                a3s.Key = mode.ToByteArray();
                a3s.IV = a3s1V;

                var encr3ypt0r = a3s.CreateEncryptor(a3s.Key, a3s.IV);

                using (var bufferStream = new System.IO.MemoryStream())
                {
                    using (var crypt0Stream = new System.Security.Cryptography.CryptoStream(bufferStream, encr3ypt0r, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new System.IO.StreamWriter(crypt0Stream)) streamWriter.Write(vendoEnanitosVerdes);
                    }

                    result = System.Convert.ToBase64String(bufferStream.ToArray());
                }
            }

            var defaultUser = new Entities.User
            {
                Id = userId,
                Password = vendoEnanitosVerdes.ToHashedPassword(),
                Enabled = true,
                HasFullAccess = true,
                PasswordChangeRequired = true,
                Identity = new Identity.Persistence.Entities.PersonIdentity
                {
                    Name = userId,
                    Sex = Identity.Persistence.Enumerations.Sex.Unknown,
                    CivilStatus = Identity.Persistence.Enumerations.CivilStatus.Unknow
                }
            };

            defaultUser.Signature = Helpers.Integrity.UserIntegrityHelper.GetSignature(defaultUser);

            target.Users.Add(defaultUser);

            if (target is Core.Persistence.ORM.EF.Context.Contract.AbstractTraceableDbContext)
            {
                (target as Core.Persistence.ORM.EF.Context.Contract.AbstractTraceableDbContext).SaveChanges(persistenceChangeAuthor ?? "system");
            }
            else
            {
                target.SaveChanges();
            }

            return result;
        }
    }
}