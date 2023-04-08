using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class MunicipalityInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Municipality, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "State")]
        public System.Guid? StateId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> States { get; set; }

        public Persistence.Entities.Municipality Export()
        {
            var entity = new Persistence.Entities.Municipality();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Municipality entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            StateId = entity.StateId;
        }

        public void Mergue(Persistence.Entities.Municipality entity)
        {
            entity.Name = Name;
        }
    }
}
