using System.Linq;

namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Models.View
{
    public class UserDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.User, string>
    {
        public string Id { get; set; }

        public bool HasFullAccess { get; set; }

        public bool Enabled { get; set; }

        public string Created { get; set; }

        public bool ValidSignature { get; set; }

        public System.Collections.Generic.List<Core.Security.Common.Persistence.Entities.Role> Roles { get; set; }

        public void Import(Persistence.Entities.User entity)
        {
            Id = entity.Id;
            HasFullAccess = entity.HasFullAccess;
            Enabled = entity.Enabled;
            Created = entity.Created.ToString();
            ValidSignature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.IsCorrectlySigned(entity);
            Roles = entity.RoleBridges.Select(bridge => bridge.Stern).ToList();
        }
    }
}
