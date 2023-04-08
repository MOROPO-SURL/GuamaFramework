namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class RegionDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Region, System.Guid>
    {
        public RegionDisplayViewModel()
        {
            Country = new CountryDisplayViewModel();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public CountryDisplayViewModel Country { get; set; }

        public void Import(Persistence.Entities.Region entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            Country.Import(entity.Country);
        }
    }
}
