using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class LocalityInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Locality, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Municipality")]
        public System.Guid? MunicipalityId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Municipalities { get; set; }

        public Persistence.Entities.Locality Export()
        {
            var entity = new Persistence.Entities.Locality();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Locality entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            MunicipalityId = entity.MunicipalityId;
        }

        public void Mergue(Persistence.Entities.Locality entity)
        {
            entity.Name = Name;
        }
    }
}
