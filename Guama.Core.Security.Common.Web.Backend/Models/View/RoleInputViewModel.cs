using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Models.View
{
    public class RoleInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Role, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), StringLength(50, MinimumLength = 3, ErrorMessage = "StringLengthMinMaxValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Permissions")]
        public SelectionDescriptedItemInputViewModel<int>[] UngroupedSelectablePermissions { get; set; }

        public KeyValuePair<string, SelectionDescriptedItemInputViewModel<int>[]>[] GroupedSelectablePermissions { get; set; }

        public void Import(Persistence.Entities.Role entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Persistence.Entities.Role entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }

        public Persistence.Entities.Role Export()
        {
            var entity = new Persistence.Entities.Role();

            Mergue(entity);

            return entity;
        }
    }
}
