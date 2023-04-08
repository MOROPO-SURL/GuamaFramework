namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class CountryDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Country, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string IsoCode { get; set; }

        public void Import(Persistence.Entities.Country entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            IsoCode = entity.IsoCode;
        }
    }
}
