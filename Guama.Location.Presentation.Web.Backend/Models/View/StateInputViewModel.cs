using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class StateInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.State, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Region")]
        public System.Guid? RegionId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Regions { get; set; }

        public Persistence.Entities.State Export()
        {
            var entity = new Persistence.Entities.State();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.State entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            RegionId = entity.RegionId;
        }

        public void Mergue(Persistence.Entities.State entity)
        {
            entity.Name = Name;
        }
    }
}
