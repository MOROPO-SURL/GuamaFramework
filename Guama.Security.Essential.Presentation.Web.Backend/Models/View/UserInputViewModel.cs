using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Models.View
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

        public void Import(Persistence.Entities.User entity)
        {
            Id = entity.Id;
            HasFullAccess = entity.HasFullAccess;
            Enabled = entity.Enabled;
            PasswordChangeRequired = entity.PasswordChangeRequired;
        }

        public void Mergue(Persistence.Entities.User entity)
        {
            entity.HasFullAccess = HasFullAccess;
            entity.Enabled = Enabled;
            entity.PasswordChangeRequired = PasswordChangeRequired;

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
