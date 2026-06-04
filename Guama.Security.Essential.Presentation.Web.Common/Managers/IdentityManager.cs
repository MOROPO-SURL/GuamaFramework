using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Extensions;
using System.Linq;
using System.Security.Claims;

namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Common.Managers
{
    public class IdentityManager : Core.Presentation.Web.Security.Managers.Facade.IIdentityManagerFacade
    {
        private readonly ILogger<IdentityManager> logger;
        private readonly Core.Persistence.Repositories.ICommandEntityRepository<Persistence.Entities.User, string> userRepository;

        public IdentityManager(Core.Persistence.Repositories.ICommandEntityRepository<Persistence.Entities.User, string> userRepository, ILogger<IdentityManager> logger)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public ClaimsIdentity ProvideIdentity(string username, string password, string authenticationType)
        {
            try
            {
                var entity = userRepository.Get(username);

                if (entity == null || !Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.IsCorrectlySigned(entity)) return null;

                if (entity.Enabled && !entity.Password.Equals(password.ToHashedPassword()))
                {
                    entity.FailAuthenticationCount = entity.FailAuthenticationCount + 1;

                    userRepository.Update(entity, username);

                    if (entity.FailAuthenticationCount > GetAuthenticationFailCountLimit())
                    {
                        entity.FailAuthenticationCount = 0;
                        entity.Enabled = false;

                        entity.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(entity);

                        userRepository.Update(entity, username);

                        logger.LogInformation("User '{0}' reach fail authentication count limit ({1}) and was authomatically disabled.", username, GetAuthenticationFailCountLimit());

                        return null;
                    }

                    if (entity.FailAuthenticationCount > GetAuthenticationFailCountTolerance())
                    {
                        throw new Core.Presentation.Web.Security.Exceptions.ForceAuthenticationDetectException();
                    }

                    return null;
                }

                var claims = new System.Collections.Generic.List<Claim>();

                ConfigureClaims(entity, claims);

                if (entity.Enabled)
                {
                    entity.FailAuthenticationCount = 0;
                    entity.LastSessionDate = System.DateTime.Now;

                    userRepository.Update(entity, username);
                }

                return new ClaimsIdentity(claims, authenticationType);
            }
            catch { return null; }
        }

        protected virtual int GetAuthenticationFailCountTolerance()
        {
            return 3;
        }

        protected virtual int GetAuthenticationFailCountLimit()
        {
            return 10;
        }

        public int GetCaptchaCodeLength()
        {
            return 8;
        }

        public virtual void ConfigureClaims(Persistence.Entities.User user, System.Collections.Generic.List<Claim> claims)
        {
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id));
            claims.Add(new Claim(Core.Presentation.Web.Security.Constants.ClaimTypes.LastLoginDateTimeTicks, user.LastSessionDate.Ticks.ToString()));

            if (!user.Enabled)
            {
                claims.Add(new Claim(Core.Presentation.Web.Security.Constants.ClaimTypes.AccountDisabled, true.ToString()));
            }

            if (user.HasFullAccess)
            {
                claims.Add(new Claim(Core.Presentation.Web.Security.Constants.ClaimTypes.AccountWithFullAccess, true.ToString()));
            }

            if (user.PasswordChangeRequired)
            {
                claims.Add(new Claim(Core.Presentation.Web.Security.Constants.ClaimTypes.AccountChangePasswordRequired, true.ToString()));
            }

            var validUserRoles = user.RoleBridges.ToArray().Where(item => Persistence.ORM.EF.Helpers.Integrity.UserRoleBridgeIntegrityHelper.IsCorrectlySigned(item)).Select(item => item.Stern).ToArray();

            foreach (var role in validUserRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Id.ToString()));
            }
        }

        public bool TryModifyPassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                var user = userRepository.Get(username);

                if (!user.Password.Equals(oldPassword.ToHashedPassword())) return false;

                user.Password = newPassword.ToHashedPassword();
                user.PasswordChangeRequired = false;

                user.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(user);

                userRepository.Update(user, username);

                return true;
            }
            catch { return false; }
        }
    }
}
