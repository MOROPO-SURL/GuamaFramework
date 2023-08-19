using System.Linq;

namespace MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View
{
    public class UserDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.User, string>
    {
        public string Id { get; set; }

        public bool HasFullAccess { get; set; }

        public bool Enabled { get; set; }

        public string Created { get; set; }

        public bool ValidSignature { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public System.DateTime? BornDate { get; set; }

        public Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        public Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public string About { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public System.Collections.Generic.List<Core.Security.Common.Persistence.Entities.Role> Roles { get; set; }

        public void Import(Persistence.Entities.User entity)
        {
            Id = entity.Id;
            HasFullAccess = entity.HasFullAccess;
            Enabled = entity.Enabled;
            Created = entity.Created.ToString();
            ValidSignature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.IsCorrectlySigned(entity);
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            Alias = entity.Identity.Alias;
            BornDate = entity.Identity.BornDate;
            CivilStatus = entity.Identity.CivilStatus;
            Sex = entity.Identity.Sex;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId;
            ImageExtension = entity.Identity.Image == null? null : MimeTypes.MimeTypeMap.GetExtension(entity.Identity.Image.ContentType);
            Roles = entity.RoleBridges.Select(bridge => bridge.Stern).ToList();
        }
    }
}
