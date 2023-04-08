using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class RegionInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Region, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Country")]
        public System.Guid? CountryId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Countries { get; set; }

        public Persistence.Entities.Region Export()
        {
            var entity = new Persistence.Entities.Region();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Region entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            CountryId = entity.CountryId;
        }

        public void Mergue(Persistence.Entities.Region entity)
        {
            entity.Name = Name;
        }
    }
}
