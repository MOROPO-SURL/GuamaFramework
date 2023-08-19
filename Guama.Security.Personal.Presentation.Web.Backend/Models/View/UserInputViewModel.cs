using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View
{
    public class UserInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.User, string>
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage"), StringLength(50, MinimumLength = 3, ErrorMessage = "StringLengthMinMaxValidationErrorMessage")]
        [Display(Name = "Login")]
        public string Id { get; set; }

        [Display(Name = "Roles")]
        public List<Core.Presentation.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> SelectableRoles { get; set; }

        public bool HasFullAccess { get; set; }

        public bool Enabled { get; set; }

        public bool PasswordChangeRequired { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "BornDate")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "CivilStatus")]
        public Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Sex")]
        public Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 1048576, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public bool ResetImage { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public void Import(Persistence.Entities.User entity)
        {
            Id = entity.Id;
            HasFullAccess = entity.HasFullAccess;
            Enabled = entity.Enabled;
            PasswordChangeRequired = entity.PasswordChangeRequired;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            Alias = entity.Identity.Alias;
            BornDate = entity.Identity.BornDate;
            CivilStatus = entity.Identity.CivilStatus;
            Sex = entity.Identity.Sex;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId;
            ImageExtension = entity.Identity.Image == null ? null : MimeTypes.MimeTypeMap.GetExtension(entity.Identity.Image.ContentType);
        }

        public void Mergue(Persistence.Entities.User entity)
        {
            entity.HasFullAccess = HasFullAccess;
            entity.Enabled = Enabled;
            entity.PasswordChangeRequired = PasswordChangeRequired;
            entity.Identity.Name = Name;
            entity.Identity.LastName = LastName;
            entity.Identity.Alias = Alias;
            entity.Identity.BornDate = BornDate;
            entity.Identity.Sex = Sex;
            entity.Identity.CivilStatus = CivilStatus;
            entity.Identity.About = About;

            entity.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(entity);
        }

        public virtual Persistence.Entities.User Export()
        {
            var entity = new Persistence.Entities.User
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }
    }
}
