using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class CountryInputViewModel : Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Country, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "IsoCode")]
        public string IsoCode { get; set; }

        public Persistence.Entities.Country Export()
        {
            var entity = new Persistence.Entities.Country();

            Mergue(entity);

            return entity;

        }

        public void Import(Persistence.Entities.Country entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            IsoCode = entity.IsoCode;
        }

        public void Mergue(Persistence.Entities.Country entity)
        {
            entity.Name = Name;
            entity.IsoCode = IsoCode;
        }
    }
}
